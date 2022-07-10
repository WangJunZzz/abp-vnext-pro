import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import { useI18n } from "/@/hooks/web/useI18n";

const { t } = useI18n();
import { formatToDateTime } from '/@/utils/dateUtil';
import { AuditLogsServiceProxy, PagingAuditLogListInput } from "/@/services/ServiceProxies";

export const searchFormSchema: FormSchema[] = [
  {
    field: "userName",
    label: t("routes.admin.userManagement_userName"),
    component: "Input",
    colProps: { span: 8 }
  },
  {
    field: "time",
    component: "RangePicker",
    label: t("routes.admin.audit_executeTime"),
    colProps: {
      span: 6
    }
  }
];

export const tableColumns: BasicColumn[] = [
  // {
  //   title: t('routes.admin.tenant'),
  //   dataIndex: 'tenantName',
  //   width: 100,
  // },
  {
    title: "Url",
    dataIndex: "url",
    width: 350,
    align: "left"
  },
  {
    title: t("routes.admin.userManagement_userName"),
    dataIndex: "userName",
    width: 100
  },
  {
    title: t("routes.admin.executionTime"),
    dataIndex: "executionTime",
    width: 200,
    customRender: ({ text }) => {
      return formatToDateTime(text);
    }
  },
  {
    title: t("routes.admin.executionDuration"),
    dataIndex: "executionDuration",
    width: 150
  },
  {
    title: "Exceptions",
    dataIndex: "exceptions",
    
  }
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

export function httpStatusCodeColor(statusCode?: number) {
  if (!statusCode) {
    return "";
  }
  if (statusCode >= 200 && statusCode < 300) {
    return "#87d068";
  }
  if (statusCode >= 300 && statusCode < 400) {
    return "#108ee9";
  }
  if (statusCode >= 400 && statusCode < 500) {
    return "orange";
  }
  if (statusCode >= 500) {
    return "red";
  }
  return "cyan";
}

export function httpMethodColor(method?: string) {
  if (method == "GET") {
    return "blue";
  }
  if (method == "POST") {
    return "blue";
  }
  if (method == "PUT") {
    return "orange";
  }
  if (method == "DELETE") {
    return "red";
  }
  if (method == "OPTIONS") {
    return "cyan";
  }
  if (method == "PATCH") {
    return "pink";
  }
  return "cyan";
}
