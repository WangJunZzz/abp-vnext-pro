import moment from 'moment'
export const Batchattribute = [
  {
    title: '生产日期',
    dataIndex: 'creationDate',
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  },
  {
    title: '失效日期',
    dataIndex: 'expirationDate',
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  },
  {
    title: '入库日期',
    dataIndex: 'stockDate',
    customRender: ({ text }) => {
      return text ? moment(text).format("YYYY-MM-DD HH:mm:ss") : '';
    }
  },
  {
    title: '生产批次',
    dataIndex: 'productionBatch',
  },
  {
    title: '箱号',
    dataIndex: 'carton',
  },
  {
    title: 'Plant',
    dataIndex: 'plant',
  },
  {
    title: '批次编码',
    dataIndex: 'batchCode',
  },
  {
    title: '退货原因',
    dataIndex: 'returnReason',
  },
  {
    title: '虚仓',
    dataIndex: 'virtualWarehouse',
  },
  {
    title: '箱规',
    dataIndex: 'boxQty',
  },
  {
    title: 'PO',
    dataIndex: 'po',
  },
]
