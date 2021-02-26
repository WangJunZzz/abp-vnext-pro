import { SettingsUI } from '@/services/api'
import { request, METHOD } from '@/utils/request'

/**
 * 获取所有设置信息
 */
export function getSettingsAll() {
    return request(SettingsUI, METHOD.GET)
}

/**
 * 修改settings
 * @param {*} params 
 */
export function putSettings(params) {
    let url = SettingsUI + "/setSettingValues"
    return request(url, METHOD.PUT, params)
}
export default {
    getSettingsAll,
    putSettings
}