import { SettingsServiceProxy } from "/@/services/ServiceProxies";

/**
 * 获取所有settings
 * @returns
 */
export async function getAllSettingsAsync() {
  const _settingsServiceProxy = new SettingsServiceProxy();
  return _settingsServiceProxy.all();
}

export async function updateSettingsAsync({ request }) {
  const _settingsServiceProxy = new SettingsServiceProxy();

  return _settingsServiceProxy.update(request);
}
