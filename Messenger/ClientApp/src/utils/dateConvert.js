export default function convertDate(date){
    var d = new Date(date);
    return d.toLocaleTimeString().slice(0,5);
}