import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import {
  LanguagesServiceProxy,
  DeleteLanguageInput,
  PageLanguageInput,
} from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';

const { t } = useI18n();
// 分页表格语言 BasicColumn
export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.admin.language_cultureName'),
    dataIndex: 'cultureName',
  },
  {
    title: t('routes.admin.language_uiCultureName'),
    dataIndex: 'uiCultureName',
  },
  {
    title: t('routes.admin.language_displayName'),
    dataIndex: 'displayName',
  },
  {
    title: t('routes.admin.language_flagIcon'),
    dataIndex: 'flagIcon',
  },
  {
    title: t('common.isEnabled'),
    dataIndex: 'isEnabled',
  },
];

// 分页查询语言 FormSchema
export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: t('common.key'),
    component: 'Input',
    colProps: { span: 8 },
  },
];

// 创建语言 FormSchema
export const createFormSchema: FormSchema[] = [
  {
    field: 'cultureName',
    label: t('routes.admin.language_cultureName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'uiCultureName',
    label: t('routes.admin.language_uiCultureName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'displayName',
    label: t('routes.admin.language_displayName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'isEnabled',
    label: t('common.isEnabled'),
    component: 'Switch',
    colProps: { span: 18 },
  },
  {
    field: 'flagIcon',
    label: t('routes.admin.language_flagIcon'),
    component: 'Input',
    required: false,
    colProps: { span: 18 },
  },
];

// 编辑语言 FormSchema
export const updateFormSchema: FormSchema[] = [
  {
    field: 'id',
    label: 'Id',
    component: 'Input',
    ifShow: false,
    colProps: { span: 18 },
  },
  {
    field: 'cultureName',
    label: t('routes.admin.language_cultureName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'uiCultureName',
    label: t('routes.admin.language_uiCultureName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'displayName',
    label: t('routes.admin.language_displayName'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'isEnabled',
    label: t('common.isEnabled'),
    component: 'Switch',
    colProps: { span: 18 },
  },
  {
    field: 'flagIcon',
    label: t('routes.admin.language_flagIcon'),
    component: 'Input',
    required: false,
    colProps: { span: 18 },
  },
];

/**
 * 分页查询语言
 */
export async function pageAsync(params: PageLanguageInput) {
  const languageServiceProxy = new LanguagesServiceProxy();
  return languageServiceProxy.page(params);
}

/**
 * 创建语言
 */
export async function createAsync({ params }) {
  const languageServiceProxy = new LanguagesServiceProxy();
  await languageServiceProxy.create(params);
}

/**
 * 更新语言
 */
export async function updateAsync({ params }) {
  const languageServiceProxy = new LanguagesServiceProxy();
  await languageServiceProxy.update(params);
}

/**
 * 删除语言
 */
export async function deleteAsync({ id }) {
  const languageServiceProxy = new LanguagesServiceProxy();
  const request = new DeleteLanguageInput();
  request.id = id;
  await languageServiceProxy.delete(request);
}
