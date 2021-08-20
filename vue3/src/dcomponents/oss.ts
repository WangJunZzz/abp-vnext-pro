
import { ImportExportTaskServiceProxy } from "../services/ServiceProxies";

/**
 * 导入
 * @param request:ImportTaskInput
 * @returns
 */
export async function importAsync(request) {
  const _importTaskServiceProxy = new ImportExportTaskServiceProxy();
  return await _importTaskServiceProxy.import(request);
}
