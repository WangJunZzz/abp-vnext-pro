import { BasicColumn, FormSchema } from '/@/components/Table';
import { useI18n } from '/@/hooks/web/useI18n';
import moment from 'moment';
import { AuditServiceProxy, QueryAuditLogInput, QueryAuditLogOutputPagedResultDto, QueryEntityChangeInput, QueryEntityChangeOutput } from '/@/services/ServiceProxies';
const { t } = useI18n();
export const searchFormSchema: FormSchema[] = [
  {
    field: 'ExecutionTime',
    component: 'DatePicker',
    label: t('routes.admin.audit_executeTime'),
    colProps: {
      span: 3
    },
    componentProps: {
      valueFormat: "YYYY-MM-DD"
    }
  },
  {
    field: 'userName',
    component: 'Input',
    label: t('routes.admin.audit_userName'),
    colProps: {
      span: 4
    },
  },
  {
    field: 'httpMethod',
    component: 'Select',
    label: t('routes.admin.audit_httpMethod'),
    colProps: {
      span: 4
    },
    componentProps: {
      options: [
        {
          label: 'GET',
          value: 'GET',
          key: '1',
        }, {
          label: 'HEAD',
          value: 'HEAD',
          key: '2',
        }, {
          label: 'POST',
          value: 'POST',
          key: '3',
        }, {
          label: 'PUT',
          value: 'PUT',
          key: '4',
        }, {
          label: 'DELETE',
          value: 'DELETE',
          key: '5',
        }, {
          label: 'CONNECT',
          value: 'CONNET',
          key: '6',
        }, {
          label: 'OPTIONS',
          value: 'OPTIONS',
          key: '7',
        }, {
          label: 'TRACE',
          value: 'TRACE',
          key: '8',
        }, {
          label: 'PATH',
          value: 'PATH',
          key: '9',
        },
      ]
    }
  },
  {
    field: 'httpStatusCode',
    component: 'Select',
    label: t('routes.admin.audit_httpStatusCode'),
    colProps: {
      span: 4
    },
    componentProps: {
      options: [
        {
          label: '100-Continue',
          value: '100',
          key: '1',
        }, {
          label: '101-SwitchingProtocols',
          value: '101',
          key: '2',
        }, {
          label: '200-OK',
          value: '200',
          key: '3',
        }, {
          label: '201-Created',
          value: '201',
          key: '4',
        }, {
          label: '202-Accepted',
          value: '202',
          key: '5',
        }, {
          label: '203-Non-AuthoritativeInformation',
          value: '203',
          key: '6',
        }, {
          label: '204-NoContent',
          value: '204',
          key: '7',
        }, {
          label: '205-ResetContent',
          value: '205',
          key: '8',
        }, {
          label: '206-PartialContent',
          value: '206',
          key: '9',
        }, {
          label: '300-MultipleChoices',
          value: '300',
          key: '10',
        }, {
          label: '301-MovedPermanently',
          value: '301',
          key: '11',
        }, {
          label: '302-Found',
          value: '302',
          key: '12',
        }, {
          label: '303-SeeOther',
          value: '303',
          key: '13',
        }, {
          label: '304-NotModified',
          value: '304',
          key: '14',
        }, {
          label: '305-UseProxy',
          value: '305',
          key: '15',
        }, {
          label: '306-Unused',
          value: '306',
          key: '16',
        }, {
          label: '307-TemporaryRedirect',
          value: '307',
          key: '17',
        }, {
          label: '400-BadRequest',
          value: '400',
          key: '18',
        }, {
          label: '401-Unauthorized',
          value: '401',
          key: '19',
        }, {
          label: '402-PaymentRequired',
          value: '402',
          key: '20',
        }, {
          label: '403-Forbidden',
          value: '403',
          key: '21',
        }, {
          label: '404-NotFound',
          value: '404',
          key: '22',
        }, {
          label: '405-MethodNotAllowed',
          value: '405',
          key: '23',
        }, {
          label: '406-NotAcceptable',
          value: '406',
          key: '24',
        }, {
          label: '407-ProxyAuthenticationRequired',
          value: '407',
          key: '25',
        }, {
          label: '408-RequestTime-out',
          value: '408',
          key: '26',
        }, {
          label: '409-Conflict',
          value: '409',
          key: '27',
        }, {
          label: '410-Gone',
          value: '410',
          key: '28',
        }, {
          label: '411-LengthRequired',
          value: '411',
          key: '29',
        }, {
          label: '412-PreconditionFailed',
          value: '412',
          key: '30',
        }, {
          label: '413-RequestEntityTooLarge',
          value: '413',
          key: '31',
        }, {
          label: '414-Request-URITooLarge',
          value: '414',
          key: '32',
        }, {
          label: '415-UnsupportedMediaType',
          value: '415',
          key: '33',
        }, {
          label: '416-Requestedrangenotsatisfiable',
          value: '416',
          key: '34',
        }, {
          label: '417-ExpectationFailed',
          value: '417',
          key: '35',
        }, {
          label: '500-InternalServerError',
          value: '500',
          key: '36',
        }, {
          label: '501-NotImplemented',
          value: '501',
          key: '37',
        }, {
          label: '502-BadGateway',
          value: '502',
          key: '38',
        }, {
          label: '503-ServiceUnavailable',
          value: '503',
          key: '39',
        }, {
          label: '504-GatewayTime-out',
          value: '504',
          key: '40',
        }, {
          label: '505-HTTPVersionnotsupported',
          value: '505',
          key: '41',
        },

      ]
    }
  }
]
export const tableColumns: BasicColumn[] = [
  {
    title: t('routes.admin.audit_httpRequest'),
    dataIndex: 'httpMethod',
    slots: { customRender: 'category' }
  },
  {
    title: t('routes.admin.audit_url'),
    dataIndex: 'url',
  },
  {
    title: t('routes.admin.audit_userName'),
    dataIndex: 'userName',
  },
  {
    title: t('routes.admin.audit_ipAdrress'),
    dataIndex: 'clientIpAddress',
  },
  {
    title: t('routes.admin.audit_executeTime'),
    dataIndex: 'executionTime',
    customRender: ({ text }) => {
      return moment(text).format("YYYY-MM-DD HH:mm:ss");
    }
  },
  {
    title: t('routes.admin.audit_duration'),
    dataIndex: 'executionDuration',
  }
]
//获取表格列表
export async function getTableListAsync(params: QueryAuditLogInput): Promise<QueryAuditLogOutputPagedResultDto> {
  const _auditServiceProxy = new AuditServiceProxy();
  return await _auditServiceProxy.list(params);
}
//获取实体信息
export async function getEntityInfoAsync(id: string): Promise<QueryEntityChangeOutput[]> {

  const _auditServiceProxy = new AuditServiceProxy();
  let request = new QueryEntityChangeInput();
  request.id = id;
  return _auditServiceProxy.queryEntity(request);
}
