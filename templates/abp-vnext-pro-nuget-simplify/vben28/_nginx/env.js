function getenv(r) {
  var strEnv = '{'+
        '"Shortsha": "'+ process.env.SHORTSHA +'",'
      + '"HOSTNAME": "'+ process.env.HOSTNAME +'",'
      + '"RefName": "ValRefName",'
      + '"BuildTime": "ValBuildTime",'
      + '"CommitTitle": "ValTitle",'
      + '"CommitDescription": "ValDescription",'
      + '"PipelineUrl": "ValPipelineUrl",'
      + '"CommitUser": "ValUser",'
      + '"UI_ENVIRONMENT": "'+ process.env.UI_ENVIRONMENT +'"'
      +'}';
  r.headersOut['Content-Type'] = "application/json; charset=utf-8";
  r.return(200, strEnv);
}
