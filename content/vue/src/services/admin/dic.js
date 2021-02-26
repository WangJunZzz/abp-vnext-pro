import { Dic } from '@/services/api'
import { request, METHOD } from '@/utils/request'

/**
 * 分页获取字典信息
 */
export function getDicList(params) {
    return request(Dic, METHOD.GET, params)
}

/**
 * 分页获取字典信息
 */
export function getDicDetail(id) {
    let url = Dic + "/" + id + "/detail";
    return request(url, METHOD.GET)
}

/**
 * 新增字典
 */
export function postDic(params) {
    return request(Dic, METHOD.POST, params);
}
/**
 * 新增字典
 */
export function postDicDetail(params) {
    let url = Dic + "/detail"
    return request(url, METHOD.POST, params);
}
/**
 * 编辑字典
 */
export function putDic(params) {
    return request(Dic, METHOD.PUT, params);
}

/**
 * 编辑字典详情
 */
export function putDicDetail(params) {
    return request(Dic + "/detail", METHOD.PUT, params);
}
/**
 * 删除字典
 */
export function deleteDic(id, itemId) {
    let url = Dic + "/" + id + "?itemId=" + itemId
    return request(url, METHOD.DELETE);
}

export default {
    getDicList,
    getDicDetail,
    postDic,
    putDic,
    putDicDetail,
    deleteDic
}