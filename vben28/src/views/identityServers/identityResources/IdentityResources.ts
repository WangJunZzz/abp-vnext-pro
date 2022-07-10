import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import {
  IdentityResourceServiceProxy,
  PagingIdentityResourceListInput,
  IdInput,
} from '/@/services/ServiceProxies';
import { message } from 'ant-design-vue';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: '关键字',
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const tableColumns: BasicColumn[] = [
  {
    title: 'Name',
    dataIndex: 'name',
  },
  {
    title: 'DisplayName',
    dataIndex: 'displayName',
  },
  {
    title: t('common.enabled'),
    dataIndex: 'enabled'
  },
  {
    title: 'Description',
    dataIndex: 'description',
  },
  {
    title: 'Required',
    dataIndex: 'required'
  },
  {
    title: 'Emphasize',
    dataIndex: 'emphasize'
  },
  {
    title: 'ShowInDiscoveryDocument',
    dataIndex: 'showInDiscoveryDocument'
  },
];
export const createFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'Name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'displayName',
    label: 'DisplayName',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'enabled',
    label: 'Enabled',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'required',
    label: 'Required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'Emphasize',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'showInDiscoveryDocument',
    label: 'ShowInDiscoveryDocument',
    labelWidth: 200,
    component: 'Switch',
    colProps: { span: 20 },
  },
];

export const editFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: 'Name',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
    componentProps: {
      disabled: true,
    },
  },
  {
    field: 'displayName',
    label: 'DisplayName',
    component: 'Input',
    required: true,
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'enabled',
    label: 'Enabled',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'description',
    label: 'Description',
    component: 'Input',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'required',
    label: 'Required',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'emphasize',
    label: 'Emphasize',
    component: 'Switch',
    labelWidth: 200,
    colProps: { span: 20 },
  },
  {
    field: 'showInDiscoveryDocument',
    label: 'ShowInDiscoveryDocument',
    labelWidth: 200,
    component: 'Switch',
    colProps: { span: 20 },
  },
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingIdentityResourceListInput) {
  const _identityResourceServiceProxy = new IdentityResourceServiceProxy();
  return _identityResourceServiceProxy.page(params);
}
export async function createIdentityResourcesAsync({
  request,
  changeOkLoading,
  validate,
  closeModal,
}) {
  changeOkLoading(true);
  await validate();
  const _identityResourceServiceProxy = new IdentityResourceServiceProxy();
  await _identityResourceServiceProxy.create(request);
  changeOkLoading(false);
  closeModal();
}
/**
 * 删除
 * @param param0
 */
export async function deleteIdentityResourcesAsync({ id, reload }) {
  const _identityResourceServiceProxy = new IdentityResourceServiceProxy();
  const request = new IdInput();
  request.id = id;
  await _identityResourceServiceProxy.delete(request);
  reload();
}

export async function editIdentityResourceAsync({ request, changeOkLoading, closeModal }) {
  try {
    changeOkLoading(true);
    const _identityResourceServiceProxy = new IdentityResourceServiceProxy();
    await _identityResourceServiceProxy.update(request);
    changeOkLoading(false);
    message.success(t('common.operationSuccess'));
    closeModal();
  } catch (error) {
  } finally {
    changeOkLoading(false);
  }
}
