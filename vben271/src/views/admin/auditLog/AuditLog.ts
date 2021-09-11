import { FormSchema } from '/@/components/Table';
import { BasicColumn } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
const { t } = useI18n();
import moment from 'moment';
import { AuditLogsServiceProxy, PagingAuditLogListInput } from '/@/services/ServiceProxies';
export const searchFormSchema: FormSchema[] = [
  {
    field: 'userName',
    label: t('routes.admin.userManagement_userName'),
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'time',
    component: 'RangePicker',
    label: t('routes.admin.audit_executeTime'),
    colProps: {
      span: 6,
    },
  },
];

export const tableColumns: BasicColumn[] = [
  // {
  //   title: t('routes.admin.tenant'),
  //   dataIndex: 'tenantName',
  //   width: 100,
  // },
  {
    title: t('routes.admin.userManagement_userName'),
    dataIndex: 'userName',
    width: 100,
  },
  {
    title: 'HttpMethod',
    dataIndex: 'httpMethod',
    width: 100,
  },
  {
    title: 'HttpStatusCode',
    dataIndex: 'httpStatusCode',
    width: 120,
  },
  {
    title: 'Url',
    dataIndex: 'url',
    width: 350,
  },
  {
    title: 'ExecutionTime',
    dataIndex: 'executionTime',
    width: 150,
    customRender: ({ text }) => {
      return moment(text).format('YYYY-MM-DD HH:mm:ss');
    },
  },
  {
    title: 'BrowserInfo',
    dataIndex: 'browserInfo',
  },
  {
    title: 'Exceptions',
    dataIndex: 'exceptions',
  },
];
/**
 * 分页列表
 * @param params
 * @returns
 */
export async function getTableListAsync(params: PagingAuditLogListInput) {
  const _auditLogsServiceProxy = new AuditLogsServiceProxy();
  return _auditLogsServiceProxy.page(params);
}
