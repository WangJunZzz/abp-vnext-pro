import { FilesServiceProxy, FileTokenOutput } from "/@/services/ServiceProxies";
import OSS from "ali-oss";
import { dateUtil } from '/@/utils/dateUtil';
export async function importFileAsync({ request }) {
  const _filesServiceProxy = new FilesServiceProxy();
  await _filesServiceProxy.create(request);
}

async function getFileTokenAsync() {
  const _filesServiceProxy = new FilesServiceProxy();
  const result = await _filesServiceProxy.getFileToken();
  sessionStorage.setItem("oss", JSON.stringify(result));
  return result;
}

export async function getOSSClient(): Promise<OSS> {
  let fileToken = new FileTokenOutput();
  try {
    let oss = sessionStorage.getItem("oss");
    if (!oss) {
      fileToken = await getFileTokenAsync();
    } else {
      fileToken = JSON.parse(oss);
      let expiration = dateUtil(fileToken.expiration).format("YYYY-MM-DD HH:mm:ss");
      if (expiration < formatDate(dateUtil())) {
        fileToken = await getFileTokenAsync();
      }
    }
    let request = {
      region: fileToken.region,
      accessKeyId: fileToken.accessKeyId,
      accessKeySecret: fileToken.accessKeySecret,
      bucket: fileToken.bucket,
      stsToken: fileToken.token as string
    };

    return Promise.resolve<OSS>(new OSS(request));
  } catch (error) {
    console.error("获取oss Client失败");
  }
}

export async function downLoadFile(fileName) {
  let oss = await getOSSClient();
  const response = {
    "content-disposition": `attachment; filename=${encodeURIComponent(fileName)}`
  };
  const url = (oss as OSS).signatureUrl(fileName, { response });
  let link = document.createElement("a"); //创建a标签
  link.style.display = "none"; //使其隐藏
  link.href = url; //赋予文件下载地址
  link.setAttribute("download", fileName); //设置下载属性 以及文件名
  document.body.appendChild(link); //a标签插至页面中
  link.click(); //强制触发a标签事件
}

function formatDate(dateTime) {
  const date = new Date(dateTime);
  const YY = date.getFullYear() + "-";
  const MM = (date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1) + "-";
  const DD = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
  const hh = date.getHours() + ":";
  const mm = date.getMinutes() + ":";
  const ss = date.getSeconds();
  return YY + MM + DD + " " + hh + mm + ss;
}
