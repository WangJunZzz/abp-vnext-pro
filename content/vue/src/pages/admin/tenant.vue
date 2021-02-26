<template>
  <div>
    <a-card :bordered="false">
      <div style="display: flex; flex-wrap: wrap">
        <a-input
          style="margin-left: 16px; width: 440px"
          placeholder=""
          v-model="queryParam.filter"
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
          size="small"
          @click="showAddTenantModal"
          v-if="isGranted('AbpTenantManagement.Tenants.Create')"
          >{{ $t("add") }}</a-button
        >
      </div>
    </a-card>
    <a-card style="margin-top: 24px" :bordered="false" title="">
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
        <span slot="action" slot-scope="text, record">
          <a
            href="javascript:void(0)"
            v-if="isGranted('AbpTenantManagement.Tenants.Update')"
            @click="showEditenantModal(text, record)"
            >{{ $t("edit") }}</a
          >
        </span>
      </a-table>
    </a-card>

    <!-- 新增角色 -->
    <a-modal
      v-model="showAddTenantVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="addTenantSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="addTenantParams.name" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('email')">
          <a-input v-model="addTenantParams.adminEmailAddress" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('password')">
          <a-input v-model="addTenantParams.adminPassword" size="small" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 编辑角色 -->
    <a-modal
      v-model="showEditTenantVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="editTenantSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="editTenantParams.name" size="small" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>
  </div>
</template>

<script>
import {
  getAbpTenantList,
  postAbpTenant,
  putAbpTenant,
} from "@/services/admin/tenant";

export default {
  i18n: require("./i18n"),
  data() {
    return {
      columns: [
        { title: this.$t("name"), dataIndex: "name", key: "name" },
        {
          title: this.$t("action"),
          key: "action",
          width: 150,
          scopedSlots: { customRender: "action" },
        },
      ],
      tableList: [],
      queryParam: {
        filter: "",
        sorting: "id",
        skipCount: 0,
        maxResultCount: 10,
      },
      pagination: {
        total: 0,
        pageSize: 10, // 默认每页显示数量
        showSizeChanger: true, // 显示可改变每页数量
        pageSizeOptions: ["10", "20", "30", "40"], // 每页数量选项
        //showTotal: total => `共 ${total} 条`, // 显示总数
      },
      showAddTenantVisible: false,
      showEditTenantVisible: false,
      addTenantParams: {
        name: "",
        adminEmailAddress: "",
        adminPassword: "",
      },
      editTenantParams: {
        name: "",
      },
      editTenantId: "",
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.loading = true;
      getAbpTenantList(this.queryParam).then((res) => {
        const pagination = {
          ...this.pagination,
        };
        pagination.total = res.data.totalCount;
        this.tableList = res.data.items;
        this.loading = false;
        this.pagination = pagination;
      });
    },
    onSearch() {
      this.getList();
    },
    handleTableChange(pagination) {
      this.pagination.current = pagination.current;
      this.pagination.pageSize = pagination.pageSize;
      this.queryParam.skipCount =
        (pagination.current - 1) * pagination.pageSize;
      this.queryParam.maxResultCount = pagination.pageSize;
      this.getList();
    },
    showAddTenantModal() {
      this.resetAddTenantParams();
      this.showAddTenantVisible = true;
    },
    showEditenantModal(text, row) {
      this.editTenantId = row.id;
      this.editTenantParams.name = row.name;
      this.showEditTenantVisible = true;
    },
    resetAddTenantParams() {
      this.addTenantParams.name = "";
      this.addTenantParams.adminEmailAddress = "";
      this.addTenantParams.adminPassword = "";
    },
    beforeSave(type) {
      if (type == "add") {
        if (!this.addTenantParams.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
        if (!this.addTenantParams.adminEmailAddress) {
          this.$message.error(this.$t("inputPrefix") + this.$t("email"));
          return false;
        }
        if (!this.addTenantParams.adminPassword) {
          this.$message.error(this.$t("inputPrefix") + this.$t("password"));
          return false;
        }
      } else if (type == "edit") {
        if (!this.editTenantParams.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      }
      return true;
    },
    addTenantSave() {
      if (!this.beforeSave("add")) return;
      postAbpTenant(this.addTenantParams).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.showAddTenantVisible = false;
        this.getList();
      });
    },
    editTenantSave() {
      if (!this.beforeSave("edit")) return;
      putAbpTenant(this.editTenantId, this.editTenantParams).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.showEditTenantVisible = false;
        this.getList();
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
</style>
