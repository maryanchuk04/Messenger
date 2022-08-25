import axios from "axios"

export function uploadImage(img) {
    console.log(img)
    let body = new FormData()
    body.set('key','f13e5b4582395749aa5790503930208e')
    body.append('image', img)

    return axios({
      method: 'post',
      url: 'https://api.imgbb.com/1/upload',
      data: body
    }).then((result) => {
      return result.data.data.image.url;
    })
  }