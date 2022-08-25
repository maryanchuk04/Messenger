
export class BaseService{
    _baseUrl = 'https://localhost:7023/api/';

    getUserToken = () => {
        let token = localStorage.getItem("token");
        return token === null || token === undefined
    };

    getResource = async url => {
        const call = _url => fetch(this._baseUrl + _url, {
            method: "get",
            headers: new Headers({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            }),
            credentials :"include",
        });

        let res = await call(url);
        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url);
        }
        return res;
    }

    setResource = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                credentials :"include",
                headers: new Headers({
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem("token")}`
                }),
                body: JSON.stringify(data)
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }

    setResourceWithData = async (url, data) => {
        const call = (url, data) => fetch(
            this._baseUrl + url,
            {
                method: "post",
                headers: new Headers({
                    'Authorization': `Bearer ${localStorage.getItem("token")}`
                }),
                body: data
            }
        );

        let res = await call(url, data);

        if (res.status === 401 && await this.refreshHandler()) {
            // one more try:
            res = await call(url, data);
        }

        return res;
    }


    refreshHandler = async () => {
        localStorage.removeItem("token");
        let response = await fetch('api/Token/refresh-token', {
            method: "POST"
        });

        if (!response.ok) {
            return false;
        }

        let rest = await response.json();
        localStorage.setItem("token", rest.jwtToken);

        return true;
    }
}