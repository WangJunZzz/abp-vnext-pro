<template>
  <div>
    <a-card :bordered="false">
      <div style="display: flex; flex-wrap: wrap">
        <a-input
          style="margin-left: 16px; width: 440px"
          placeholder=""
          v-model="queryParam.name"
          size="small"
        />
        <a-button
          type="primary"
          style="margin-left: 16px"
          size="small"
          @click="onSearch"
          >{{ $t("search") }}</a-button
        >
        <a-button
          type="primary"
          style="margin-left: 16px"
          @click="showAddDicModal"
          size="small"
          v-if="isGranted('Zzz.Dic.Create')"
          >{{ $t("add") }}</a-button
        >
      </div>
    </a-card>
    <a-card
      style="margin-top: 24px; background-color: #f0f2f5"
      :bordered="false"
      title=""
      :bodyStyle="{ padding: '0' }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-card title="" :bordered="false">
            <a-table
              bordered
              :columns="columns"
              :data-source="tableList"
              :pagination="pagination"
              :loading="loading"
              @change="handleTableChange"
              rowKey="id"
              size="small"
            >
              <span slot="action" slot-scope="text">
                <a-button @click="getDetail(text)" size="small">{{
                  $t("detail")
                }}</a-button>
                <a-button
                  icon="plus"
                  style="margin-left: 10px"
                  v-if="isGranted('Zzz.Dic.Create')"
                  @click="showAddDicDetailModal(text)"
                  size="small"
                />
                <a-button
                  icon="edit"
                  style="margin-left: 10px"
                  v-if="isGranted('Zzz.Dic.Update')"
                  @click="showEditDicMotal(text)"
                  size="small"
                />
                <a-button
                  icon="delete"
                  style="margin-left: 10px"
                  type="primary"
                  v-if="isGranted('Zzz.Dic.Delete')"
                  @click="deleteDic(text)"
                  size="small"
                />
              </span>
            </a-table>
          </a-card>
        </a-col>
        <a-col :span="12">
          <a-card :title="$t('detail')" :bordered="false">
            <a-table
              bordered
              :columns="detailColumns"
              :data-source="detailTableList"
              :pagination="detailPagination"
              :loading="loading"
              rowKey="id"
              size="small"
            >
              <span slot="action" slot-scope="text">
                <a-button
                  icon="edit"
                  style="margin-left: 10px"
                  v-if="isGranted('Zzz.Dic.Update')"
                  @click="showEdiDicDetailMotal(text)"
                  size="small"
                />
                <a-button
                  icon="delete"
                  style="margin-left: 10px"
                  type="primary"
                  v-if="isGranted('Zzz.Dic.Delete')"
                  @click="deleteDicDetail(text)"
                  size="small"
                />
              </span>
            </a-table>
          </a-card>
        </a-col>
      </a-row>
    </a-card>

    <!-- 新增字典 -->
    <a-modal
      v-model="addDicVisible"
      :title="$t('add')"
      :destroyOnClose="true"
      @ok="addDicSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="addDicParam.name" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('description')">
          <a-input v-model="addDicParam.description" size="small" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 新增字典详情 -->
    <a-modal
      v-model="addDicDetailVisible"
      :title="$t('add')"
      :destroyOnClose="true"
      @ok="addDicDetailSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="addDicDetailParam.label" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('value')">
          <a-input v-model="addDicDetailParam.value" size="small" />
        </a-form-model-item>

        <a-form-model-item :label="$t('sort')">
          <a-input-number
            id="inputNumber"
            v-model="addDicDetailParam.sort"
            :min="1"
            :max="99999"
            size="small"
          />
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 编辑字典详情 -->
    <a-modal
      v-model="editDicDetailVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="editDicDetailSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="editDicDetailParam.label" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('value')">
          <a-input v-model="editDicDetailParam.value" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('sort')">
          <a-input-number
            id="inputNumber"
            v-model="editDicDetailParam.sort"
            :min="1"
            :max="99999"
            size="small"
          />
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 编辑字典 -->
    <a-modal
      v-model="editDicVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="editDicSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="editDicParam.name" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('description')">
          <a-input v-model="editDicParam.description" size="small" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>
  </div>
</template>

<script>
import {
  getDicList,
  getDicDetail,
  deleteDic,
  postDic,
  postDicDetail,
  putDic,
  putDicDetail,
} from "@/services/admin/dic";

export default {
  i18n: require("./i18n"),
  data() {
    return {
      columns: [
        { title: this.$t("name"), dataIndex: "name", key: "name" },
        {
          title: this.$t("description"),
          dataIndex: "description",
          key: "description",
        },
        {
          title: this.$t("action"),
          key: "action",
          width: 200,
          scopedSlots: { customRender: "action" },
        },
      ],
      detailColumns: [
        { title: this.$t("lable"), dataIndex: "label", key: "label" },
        {
          title: this.$t("value"),
          dataIndex: "value",
          key: "value",
        },
        {
          title: this.$t("action"),
          key: "action",
          width: 150,
          scopedSlots: { customRender: "action" },
        },
      ],
      tableList: [],
      detailTableList: [],
      queryParam: {
        name: "",
        sorting: "id",
        skipCount: 0,
        maxResultCount: 10,
      },
      addDicParam: {
        name: "",
        description: "",
      },
      editDicParam: {
        id: "",
        name: "",
        description: "",
      },
      addDicDetailParam: {
        id: "",
        label: "",
        value: "",
        sort: 1,
      },
      editDicDetailParam: {
        id: "",
        label: "",
        value: "",
        sort: 1,
      },
      addDicVisible: false,
      addDicDetailVisible: false,
      editDicVisible: false,
      editDicDetailVisible: false,
      editDicId: "",
      pagination: {
        total: 0,
        pageSize: 10, // 默认每页显示数量
        showSizeChanger: true, // 显示可改变每页数量
        pageSizeOptions: ["10", "20", "30", "40"], // 每页数量选项
        //showTotal: (total) => `共 ${total} 条`, // 显示总数
      },
      detailPagination: {
        total: 0,
        pageSize: 10, // 默认每页显示数量
        showSizeChanger: true, // 显示可改变每页数量
        pageSizeOptions: ["10", "20", "30", "40"], // 每页数量选项
        //showTotal: (total) => `共 ${total} 条`, // 显示总数
      },
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.loading = true;
      getDicList(this.queryParam).then((res) => {
        const pagination = {
          ...this.pagination,
        };
        res = res.data;
        pagination.total = res.data.totalCount;
        this.tableList = res.data.items;
        this.loading = false;
        this.pagination = pagination;
      });
    },
    onSearch() {
      this.detailTableList = [];
      this.getList();
    },
    getDetail(row) {
      this.editDicId = row.id;
      this.getDicDetailInfo(row.id);
    },
    getDicDetailInfo(id) {
      getDicDetail(id).then((res) => {
        res = res.data;
        this.detailTableList = res.data.items;
        const pagination = {
          ...this.detailPagination,
        };
        pagination.total = res.data.totalCount;
        this.detailPagination = pagination;
      });
    },
    deleteDic(row) {
      deleteDic(row.id).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    deleteDicDetail(row) {
      deleteDic(this.editDicId, row.id).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.getDicDetailInfo(this.editDicId);
      });
    },
    resetAddDicParams() {
      this.addDicParam.name = "";
      this.addDicParam.description = "";
      this.addDicVisible = false;
    },
    resetAddDicDetailParams() {
      this.addDicDetailParam.label = "";
      this.addDicDetailParam.value = "";
      this.addDicDetailParam.description = "";
      this.addDicDetailParam.sort = 1;
      this.addDicDetailVisible = false;
    },
    handleTableChange(pagination) {
      this.pagination.current = pagination.current;
      this.pagination.pageSize = pagination.pageSize;
      this.queryParam.skipCount =
        (pagination.current - 1) * pagination.pageSize;
      this.queryParam.maxResultCount = pagination.pageSize;
      this.getList();
    },
    showAddDicModal() {
      this.addDicVisible = true;
    },
    showAddDicDetailModal(row) {
      this.addDicDetailParam.id = row.id;
      this.addDicDetailVisible = true;
    },
    showEditDicMotal(row) {
      this.editDicVisible = true;
      this.editDicParam.id = row.id;
      this.editDicParam.name = row.name;
      this.editDicParam.description = row.description;
    },
    showEdiDicDetailMotal(row) {
      this.editDicDetailVisible = true;
      this.editDicDetailParam.id = this.editDicId;
      this.editDicDetailParam.detailId = row.id;
      this.editDicDetailParam.label = row.label;
      this.editDicDetailParam.value = row.value;
      this.editDicDetailParam.value = row.value;
      this.editDicDetailParam.sort = row.sort;
    },
    beforeSave(type) {
      if (type == "add") {
        if (!this.addDicParam.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      } else if (type == "detail") {
        if (!this.addDicDetailParam.label) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
        if (!this.addDicDetailParam.value) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      } else if (type == "edit_detail") {
        if (!this.editDicDetailParam.label) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
        if (!this.editDicDetailParam.value) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      } else if (type == "edit") {
        if (!this.editDicParam.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      }
      return true;
    },
    addDicSave() {
      if (!this.beforeSave("add")) return;
      postDic(this.addDicParam).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.resetAddDicParams();
        this.getList();
      });
    },
    addDicDetailSave() {
      if (!this.beforeSave("detail")) return;
      postDicDetail(this.addDicDetailParam).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.resetAddDicDetailParams();
        this.getDicDetailInfo(this.addDicDetailParam.id);
        this.getList();
      });
    },
    editDicSave() {
      if (!this.beforeSave("edit")) return;
      putDic(this.editDicParam).then(() => {
        this.editDicVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    editDicDetailSave() {
      if (!this.beforeSave("edit_detail")) return;
      putDicDetail(this.editDicDetailParam).then(() => {
        this.editDicDetailVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getDicDetailInfo(this.editDicId);
      });
    },
  },
};
</script>

<style lang="less" scoped>
.list-content-item {
  color: @text-color-second;
  display: inline-block;
  vertical-align: middle;
  font-size: 14px;
  margin-left: 40px;

  span {
    line-height: 20px;
  }

  p {
    margin: 4px 0 0;
    line-height: 22px;
  }
}
.ant-col-14 {
  width: 150px;
}
</style>
