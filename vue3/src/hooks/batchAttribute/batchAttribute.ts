import moment, { Moment } from 'moment'
import { FormSchema } from '/@/components/Form';
import { BasicColumn } from '/@/components/Table';
export const batchColumns: BasicColumn[] = [
  {
    title: '生产日期',
    dataIndex: 'lotAttrValueDto.productionDate',
    width: 150,
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  }, {
    title: '失效日期',
    dataIndex: 'lotAttrValueDto.expireDate',
    width: 150,
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  }, {
    title: '入库日期',
    dataIndex: 'lotAttrValueDto.entryDate',
    width: 150,
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  }, {
    title: '生产批次',
    dataIndex: 'lotAttrValueDto.productionBatch',
    width: 80
  }, {
    title: '箱号',
    dataIndex: 'lotAttrValueDto.boxNo',
    width: 80
  }, {
    title: 'Plant',
    dataIndex: 'lotAttrValueDto.plant',
    width: 80
  }, {
    title: '批次编码',
    dataIndex: 'lotAttrValueDto.batchCode',
    width: 80
  }, {
    title: '退货原因',
    dataIndex: 'lotAttrValueDto.returnReason',
    width: 150,
  }, {
    title: '虚仓',
    dataIndex: 'lotAttrValueDto.virtualWarehouse',
    width: 80
  }, {
    title: '箱规',
    dataIndex: 'lotAttrValueDto.boxQty',
    width: 80
  }, {
    title: 'PO',
    dataIndex: 'lotAttrValueDto.po',
    width: 80
  },
]

export const skuOutAttributeFormSchema: FormSchema[] = [
  {
    field: 'productionDate',
    component: 'DatePicker',
    label: '生产日期:',
    labelWidth: 80,
    componentProps: {

      customRender: ({ text }) => {
        return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
      },
      disabledDate: (current: Moment) => {
        return current && current < moment().endOf('day');
      },
    },
    colProps: {
      span: 6
    },
  },
  {
    field: 'expireDate',
    component: 'DatePicker',
    label: '失效日期:',
    labelWidth: 80,
    componentProps: {
      customRender: ({ text }) => {
        return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
      },
      disabledDate: (current: Moment) => {
        return current && current < moment().endOf('day');
      },
    },
    colProps: {
      span: 6
    },
  },
  {
    field: 'expireDate',
    component: 'DatePicker',
    label: '入库日期:',
    labelWidth: 80,
    componentProps: {

      customRender: ({ text }) => {
        return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
      },
      disabledDate: (current: Moment) => {
        return current && current < moment().endOf('day');
      },
    },
    colProps: {
      span: 6
    },
  },
  {
    field: 'productionBatch',
    component: 'Input',
    label: '生产批次:',
    colProps: {
      span: 6
    },
  },
  {
    field: 'boxNo',
    component: 'Input',
    label: '箱号:',
    colProps: {
      span: 6
    },
  },
  {
    field: 'plant',
    component: 'Input',
    label: 'Plant:',
    colProps: {
      span: 4
    },
  },
  {
    field: 'batchCode',
    component: 'Input',
    label: '批次编码',
    colProps: {
      span: 6
    },
  },
  {
    field: 'returnReason',
    component: 'Input',
    label: '退货原因:',
    colProps: {
      span: 6
    },

  },
  {
    field: 'virtualWarehouse',
    component: 'Input',
    label: '虚仓:',
    colProps: {
      span: 6
    },
  },
  {
    field: 'boxQty',
    component: 'Input',
    label: '箱规:',
    colProps: {
      span: 6
    },
  },
  {
    field: 'po',
    component: 'Input',
    label: 'PO:',
    colProps: {
      span: 24
    },
  },
]


export const attributeDataColumns: any[] = [
  {
    attr: '生产日期',
    enAttr: "productionDateVo",
    isSelected: '',
    isRequired: '',
  },
  {
    attr: '失效日期',
    enAttr: "expireDateVo",
    isSelected: '',
    isRequired: '',
  },
  {
    attr: '入库日期',
    enAttr: "entryDateVo",
    isSelected: '',
    isRequired: '',
  },
  {
    attr: '生产批次',
    enAttr: "productionBatchVo",
    isSelected: '',
    isRequired: '',
  },
  {
    attr: 'Plant',
    enAttr: "plantVo",
    isSelected: '',
    isRequired: '',
  },
  {
    attr: '箱号',
    enAttr: "boxNoVo",
    isSelected: '',
    isRequired: '',
  }, {
    attr: '批次编码',
    enAttr: "batchCodeVo",
    isSelected: '',
    isRequired: '',
  }, {
    attr: '退货原因',
    enAttr: "returnReasonVo",
    isSelected: '',
    isRequired: '',
  }, {
    attr: '虚仓',
    enAttr: "virtualWarehouseVo",
    isSelected: '',
    isRequired: '',
  }, {
    attr: '箱规',
    enAttr: "boxQtyVo",
    isSelected: '',
    isRequired: '',
  }, {
    attr: 'PO',
    enAttr: "poVo",
    isSelected: '',
    isRequired: '',
  },
]
