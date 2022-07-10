import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import { useI18n } from "/@/hooks/web/useI18n";

const { t } = useI18n();
import { formatToDateTime,dateUtil } from '/@/utils/dateUtil';
import {
  EsLogServiceProxy,
  PagingElasticSearchLogInput,
  PagingElasticSearchLogOutputCustomePagedResultDto
} from "/@/services/ServiceProxies";

export const searchFormSchema: FormSchema[] = [
  {
    field: "filter",
    component: "Input",
    label: t("common.key"),
    labelWidth: 80,
    colProps: {
      span: 6
    }
  },
  {
    field: "time",
    component: "RangePicker",
    label: "创建时间:",
    labelWidth: 80,
    colProps: { span: 6 },
    defaultValue: [dateUtil().subtract(0, "days"), dateUtil().add(1, "days")]
  }
];
export const tableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.logLevel"),
    dataIndex: "level",
    width: 200
  },
  {
    title: t("common.creationTime"),
    dataIndex: "creationTime",
    customRender: ({ text }) => {
      return formatToDateTime(text);
    },
    width: 200
  },
  {
    title: t("routes.admin.logContent"),
    dataIndex: "message"
  }
];

/**
 * ES日志
 * @param request
 * @returns
 */
export async function getElasticSearchLogAsync(
  request: PagingElasticSearchLogInput
): Promise<PagingElasticSearchLogOutputCustomePagedResultDto> {
  const _elasticSearchServiceProxy = new EsLogServiceProxy();
  return await _elasticSearchServiceProxy.page(request);
}
