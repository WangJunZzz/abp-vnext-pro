# 组件使用
## TableSelect
columns 表格列的配置描述 labelField TableSelect选中后显示的值  取值为表格数据中的key valueField:TableSelect选中后赋值value的值  取值为表格数据中的key
``` javascript
{
    field: 'customerId',
    component: 'TableSelect',
    label: '货主:',
    colProps: {
      span: 6
    },
    componentProps: ({ formModel, formActionType }) => {
      return {
        
        showSearch: true,
        columns: [
          {
            title: "货主编码",
            dataIndex: "code",
            key: "code",
            width: 200,
          }, {
            title: "名称",
            dataIndex: "name",
            key: "name",
            width: 200,
          }
        ],
        api: getWarehouseCustomerSelectorAsync,
        labelField: 'customerName', //选项选中后显示的字段名称
        valueField: 'customerId',//选项选中后返回值的字段名称
        immediate: true,
        keyWordField: 'customerName',
      };
    },
  },
```
