function getenv(r) {
    var strEnv = '{"Shortsha": "'+ process.env.SHORTSHA +'","UI_ENVIRONMENT": "'+ process.env.UI_ENVIRONMENT +'"}';
    r.headersOut['Content-Type'] = "application/json; charset=utf-8";
    r.return(200, strEnv);
}
