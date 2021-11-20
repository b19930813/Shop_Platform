//axios config
var https = require("https");
const config = {

    headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' },
    baseURL: 'https://localhost:44387',
    // timeout: 3000,
    withCredentials: true,
    responseType: 'json',
    responseEncoding: 'utf8',

}

export { config };