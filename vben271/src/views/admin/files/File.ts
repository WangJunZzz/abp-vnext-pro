import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import moment from 'moment';
import { FilesServiceProxy, PagingFileInput } from '/@/services/ServiceProxies';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
export const searchFormSchema: FormSchema[] = [
  {
    field: 'filter',
    label: t('routes.admin.fileName'),
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.admin.fileName'),
    dataIndex: 'fileName',
  },

  {
    title: t('routes.admin.userManagement_createTime'),
    dataIndex: 'creationTime',
    customRender: ({ text }) => {
      return moment(text).format('YYYY-MM-DD HH:mm:ss');
    },
  },
];

/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingFileInput) {
  const _filesServiceProxy = new FilesServiceProxy();
  return _filesServiceProxy.page(params);
}
