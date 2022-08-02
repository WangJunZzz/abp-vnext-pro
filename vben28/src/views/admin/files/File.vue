<template>
  <div>
    <BasicTable @register="registerTable" size="small">
      <template #toolbar>
        <a-upload
          v-model:file-list="fileList"
          name="file"
          :multiple="false"
          :show-upload-list="false"
          :customRequest="customRequest"
          v-auth="'AbpIdentity.FileManagement.Upload'"
        >
          <a-button>
            <upload-outlined></upload-outlined>
            上传文件
          </a-button>
        </a-upload>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'ant-design:arrow-down-outlined',
              label: t('common.download'),
              onClick: handleDownload.bind(null, record),
            },
          ]"
        />
      </template>
    </BasicTable>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { UploadOutlined } from "@ant-design/icons-vue";
import { BasicTable, useTable, TableAction } from "/@/components/Table";
import { tableColumns, searchFormSchema, getTableListAsync } from "/@/views/admin/files/File";
import { Tag } from "ant-design-vue";
import { useI18n } from "/@/hooks/web/useI18n";
import { getOSSClient, importFileAsync, downLoadFile } from "./UploadOss";
import { CreateFileInput } from "/@/services/ServiceProxies";
import { dateUtil } from '/@/utils/dateUtil';

export default defineComponent({
  components: {
    BasicTable,
    TableAction,
    Tag,
    UploadOutlined
  },
  setup() {
    const { t } = useI18n();
    const fileList = ref([]);
    // table配置
    const [registerTable, { reload }] = useTable({
      columns: tableColumns,
      formConfig: {
        labelWidth: 70,
        schemas: searchFormSchema,
        fieldMapToTime: [
          ["time", ["executionBeginTime", "executionEndTime"], "YYYY-MM-DD HH:mm:ss"]
        ]
      },
      api: getTableListAsync,
      showTableSetting: true,
      useSearchForm: true,
      bordered: true,
      canResize: true,
      showIndexColumn: true,
      actionColumn: {
        width: 120,
        title: t("common.action"),
        dataIndex: "action",
        slots: {
          customRender: "action"
        },
        fixed: "right"
      }
    });

    async function customRequest(action) {
      let fileName = action.file.name;
      let index = fileName.lastIndexOf(".");
      let type = fileName.slice(index + 1);
      let name = fileName.slice(0, index);
      const client = await getOSSClient();
      let fileFullName = `host/${dateUtil().format("YYYY-MM-DD")}/${name}_${dateUtil().format(
        "X"
      )}.${type}`;

      await client.put(fileFullName, action.file);
      let request = new CreateFileInput();
      request.fileName = fileName;
      request.filePath = fileFullName;
      await importFileAsync({ request });
      await reload();
    }

    const handleDownload = async (record: Recordable) => {
      await downLoadFile(record.filePath);
    };
    return {
      t,
      fileList,
      registerTable,
      customRequest,
      handleDownload
    };
  }
});
</script>

<style lang="less" scoped></style>
