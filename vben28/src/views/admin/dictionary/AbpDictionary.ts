import { FormSchema } from "/@/components/Table";
import { BasicColumn } from "/@/components/Table";
import { message } from "ant-design-vue";
import { useI18n } from "/@/hooks/web/useI18n";
import {
  PagingDataDictionaryInput,
  DataDictionaryServiceProxy,
  SetDataDictinaryDetailInput,
  DeleteDataDictionaryDetailInput,
  IdInput
} from "/@/services/ServiceProxies";
import { h } from "vue";
import { Switch } from "ant-design-vue";

const { t } = useI18n();
export const tableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.dictionaryCode"),
    dataIndex: "code"
  },
  {
    title: t("routes.admin.dictionaryDisplayText"),
    dataIndex: "displayText"
  },
  {
    title: t("routes.admin.dictionaryOrder"),
    dataIndex: "order"
  },
  {
    title: t("common.status"),
    dataIndex: "isEnabled",
    customRender: ({ record }) => {
      return h(Switch, {
        checked: record.isEnabled,
        checkedChildren: "是",
        unCheckedChildren: "否",
        onChange(checked: boolean) {
          const request = new SetDataDictinaryDetailInput();
          request.dataDictionaryId = record.dataDictionaryId;
          request.dataDictionayDetailId = record.id;
          request.isEnabled = checked;
          enableDictionaryAsync(request)
            .then(() => {
              record.isEnabled = checked;
              message.success(t("common.operationSuccess"));
            })
            .catch(() => {
              message.error(t("common.operationFail"));
            });
        }
      });
    }
  },
  {
    dataIndex: "description",
    title: t("routes.admin.dictionaryDescription")
  }
];
//字典类型表格
export const dictionaryTypeTableColumns: BasicColumn[] = [
  {
    title: t("routes.admin.dictionaryCode") + "|" + t("routes.admin.dictionaryDisplayText"),
    dataIndex: "text",
    align: "left"
  }
];
//字典项查询
export const searchFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: "",
    component: "Input",
    colProps: {
      span: 6
    }
  }
];

//字典类型查询
export const searchDictionaryFormSchema: FormSchema[] = [
  {
    field: "filter",
    label: "",
    component: "Input",
    colProps: {
      span: 18
    }
  }
];

//新增字典项
export const createFormSchema: FormSchema[] = [
  {
    field: "id",
    label: "",
    ifShow: false,
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "typeDisplayText",
    label: t("routes.admin.dictionaryTypeName"),
    component: "Input",
    colProps: {
      span: 18
    },
    componentProps: {
      disabled: true
    }
  },
  {
    field: "code",
    label: t("routes.admin.dictionaryCode"),
    required: true,
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "displayText",
    label: t("routes.admin.dictionaryDisplayText"),
    component: "Input",
    required: true,
    colProps: {
      span: 18
    }
  },
  {
    field: "order",
    label: t("routes.admin.dictionaryOrder"),
    required: true,
    component: "InputNumber",
    colProps: {
      span: 18
    },
    dynamicRules: () => {
      return [
        {
          required: true,
          validator: (_, value) => {
            const regNull = /^[1-9]\d*$/;
            if (regNull.test(value)) {
              return Promise.resolve();
            }
            return Promise.reject(t("routes.admin.nonZeroMessage"));
          }
        }
      ];
    }
  },
  {
    field: "description",
    label: t("routes.admin.dictionaryDescription"),
    component: "InputTextArea",
    colProps: {
      span: 18
    }
  }
];
//编辑字典项
export const editFormSchema: FormSchema[] = [
  {
    field: "dataDictionaryId",
    label: "",
    ifShow: false,
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "id",
    label: "",
    ifShow: false,
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "code",
    label: t("routes.admin.dictionaryCode"),
    required: true,
    component: "Input",
    colProps: {
      span: 18
    },
    componentProps: {
      disabled: true
    }
  },
  {
    field: "displayText",
    label: t("routes.admin.dictionaryDisplayText"),
    component: "Input",
    required: true,
    colProps: {
      span: 18
    }
  },
  {
    field: "order",
    label: t("routes.admin.dictionaryOrder"),
    required: true,
    component: "InputNumber",
    colProps: {
      span: 18
    },
    dynamicRules: () => {
      return [
        {
          required: true,
          validator: (_, value) => {
            const regNull = /^[1-9]\d*$/;
            if (regNull.test(value)) {
              return Promise.resolve();
            }
            return Promise.reject(t("routes.admin.nonZeroMessage"));
          }
        }
      ];
    }
  },
  {
    field: "description",
    label: t("routes.admin.dictionaryDescription"),
    component: "InputTextArea",
    colProps: {
      span: 18
    }
  }
];
//新增字典类型
export const createDictionaryTypeFormSchema: FormSchema[] = [
  {
    field: "code",
    label: t("routes.admin.dictionaryCode"),
    component: "Input",
    required: true,
    colProps: {
      span: 22
    }
  },
  {
    field: "displayText",
    label: t("routes.admin.dictionaryDisplayText"),
    component: "Input",
    required: true,
    colProps: {
      span: 22
    }
  },
  {
    field: "description",
    label: t("routes.admin.dictionaryDescription"),
    component: "InputTextArea",
    colProps: {
      span: 22
    }
  }
];
//编辑字典类型
export const editDictionaryTypeFormSchema: FormSchema[] = [
  {
    field: "code",
    label: t("routes.admin.dictionaryCode"),
    component: "Input",
    required: true,
    colProps: {
      span: 22
    },
    componentProps: {
      disabled: true
    }
  },
  {
    field: "displayText",
    label: t("routes.admin.dictionaryDisplayText"),
    component: "Input",
    required: true,
    colProps: {
      span: 22
    }
  },
  {
    field: "description",
    label: t("routes.admin.dictionaryDescription"),
    component: "InputTextArea",
    colProps: {
      span: 22
    }
  },
  {
    field: "key",
    label: "",
    ifShow: false,
    component: "Input",
    colProps: {
      span: 18
    }
  },
  {
    field: "id",
    label: "",
    ifShow: false,
    component: "Input",
    colProps: {
      span: 18
    }
  }
];

/**
 *获取字典类型表格
 *
 * @export
 * @return {*}
 */
export async function getDictionaryTypeAsync(params: PagingDataDictionaryInput) {
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  return await _dataDictionaryServiceProxy.page(params);
}

//新建字典类型
export async function createDictionaryTypeAsync({
  request,
  changeOkLoading,
  closeModal,
  validate,
  resetFields
}) {
  changeOkLoading(true);
  await validate();
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  await _dataDictionaryServiceProxy.create(request);
  message.success(t("common.operationSuccess"));
  resetFields();
  changeOkLoading(false);
  closeModal();
}

//编辑数据字典类型
export async function editDictionaryTypeAsync({ request, changeOkLoading, validate, closeModal }) {
  changeOkLoading(true);
  await validate();
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  await _dataDictionaryServiceProxy.update(request);
  message.success(t("common.operationSuccess"));
  changeOkLoading(false);
  closeModal();
}

//启用|禁用详情字典
export async function enableDictionaryAsync(input: SetDataDictinaryDetailInput) {
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  await _dataDictionaryServiceProxy.status(input);
}

//创建数据详情字典
export async function createDetailsDictionaryAsync({
  request,
  changeOkLoading,
  validate,
  resetFields,
  closeModal
}) {
  changeOkLoading(true);
  await validate();
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  await _dataDictionaryServiceProxy.createDetail(request);
  message.success(t("common.operationSuccess"));
  resetFields();
  changeOkLoading(false);
  closeModal();
}

//分页获取数据字典详情
export async function getDictionaryDetailsAsync({ params }) {
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();

  return await _dataDictionaryServiceProxy.pageDetail(params);
}

//编辑数据字典
export async function editDetailsDictionaryAsync({
  request,
  changeOkLoading,
  validate,
  closeModal
}) {
  changeOkLoading(true);
  await validate();
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  await _dataDictionaryServiceProxy.updateDetail(request);
  message.success(t("common.operationSuccess"));
  changeOkLoading(false);
  closeModal();
}

export async function deleteDetailAsync({ dataDictionaryId, dataDictionaryDetailId, reload }) {
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  const request = new DeleteDataDictionaryDetailInput();
  request.dataDictionaryId = dataDictionaryId;
  request.dataDictionayDetailId = dataDictionaryDetailId;
  await _dataDictionaryServiceProxy.delete(request);
  reload();
}

export async function deleteDictionaryTypeAsync({ id, reloadType }) {
  const _dataDictionaryServiceProxy = new DataDictionaryServiceProxy();
  const request = new IdInput();
  request.id = id;
  await _dataDictionaryServiceProxy.deleteDictinaryType(request);
  reloadType();
}
