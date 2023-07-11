import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import {
  LanguageTextsServiceProxy,
  PageLanguageTextInput,
  LanguagesServiceProxy,
} from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
// 分页表格语言文本 BasicColumn
export const tableColumns: BasicColumn[] = [
  {
    title: t('common.name'),
    dataIndex: 'name',
  },
  {
    title: t('common.value'),
    dataIndex: 'value',
  },
  {
    title: t('routes.admin.languageTexts_resourceName'),
    dataIndex: 'resourceName',
  },
];

// 分页查询语言文本 FormSchema
export const searchFormSchema: FormSchema[] = [
  {
    field: 'cultureName',
    label: t('routes.admin.language_cultureName'),
    labelWidth: 120,
    component: 'ApiSelect',
    defaultValue: 'zh-Hans',
    colProps: { span: 4 },
    componentProps: () => {
      return {
        api: getLanguageAsync,
        labelField: 'displayName',
        valueField: 'cultureName',
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'resourceName',
    label: t('routes.admin.languageTexts_resourceName'),
    labelWidth: 120,
    component: 'ApiSelect',
    colProps: { span: 4 },
    componentProps: () => {
      return {
        api: getResourceAsync,
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'filter',
    label: t('common.key'),
    component: 'Input',
    colProps: { span: 6 },
  },
];

// 创建语言文本 FormSchema
export const createFormSchema: FormSchema[] = [
  {
    field: 'cultureName',
    label: t('routes.admin.language_cultureName'),
    component: 'ApiSelect',
    required: true,
    colProps: { span: 18 },
    componentProps: () => {
      return {
        api: getLanguageAsync,
        labelField: 'displayName',
        valueField: 'cultureName',
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'resourceName',
    label: t('routes.admin.languageTexts_resourceName'),
    component: 'ApiSelect',
    required: true,
    colProps: { span: 18 },
    componentProps: () => {
      return {
        api: getResourceAsync,
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'name',
    label: t('common.name'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'value',
    label: t('common.value'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
];

// 编辑语言文本 FormSchema
export const updateFormSchema: FormSchema[] = [
  {
    field: 'cultureName',
    label: t('routes.admin.language_cultureName'),
    component: 'ApiSelect',
    required: true,
    colProps: { span: 18 },
    componentProps: () => {
      return {
        api: getLanguageAsync,
        labelField: 'displayName',
        valueField: 'cultureName',
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'resourceName',
    label: t('routes.admin.languageTexts_resourceName'),
    component: 'ApiSelect',
    required: true,
    colProps: { span: 18 },
    componentProps: () => {
      return {
        api: getResourceAsync,
        showSearch: true,
        optionFilterProp: 'label',
      };
    },
  },
  {
    field: 'name',
    label: t('common.name'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
  {
    field: 'value',
    label: t('common.value'),
    component: 'Input',
    required: true,
    colProps: { span: 18 },
  },
];

/**
 * 查询语言
 */
export async function getLanguageAsync() {
  const languagesServiceProxy = new LanguagesServiceProxy();
  return languagesServiceProxy.all();
}

/**
 * 查询资源
 */
export async function getResourceAsync() {
  const languageTextServiceProxy = new LanguageTextsServiceProxy();
  return languageTextServiceProxy.allResource();
}

/**
 * 分页查询语言文本
 */
export async function pageAsync(params: PageLanguageTextInput) {
  const languageTextServiceProxy = new LanguageTextsServiceProxy();
  return languageTextServiceProxy.page(params);
}

/**
 * 创建语言文本
 */
export async function createAsync({ params }) {
  const languageTextServiceProxy = new LanguageTextsServiceProxy();
  await languageTextServiceProxy.create(params);
}

/**
 * 更新语言文本
 */
export async function updateAsync({ params }) {
  const languageTextServiceProxy = new LanguageTextsServiceProxy();
  await languageTextServiceProxy.update(params);
}
