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
          @click="showAddUserModal"
          v-if="isGranted('AbpIdentity.Users.Create')"
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
            v-if="isGranted('AbpIdentity.Users.Update')"
            @click="editUserMotal(text, record)"
            >{{ $t("edit") }}</a
          >
        </span>
      </a-table>
    </a-card>

    <!-- 新增用户 -->
    <a-modal
      :bodyStyle="{ 'padding-top': '0' }"
      v-model="createUserVisible"
      :title="$t('add')"
      :destroyOnClose="true"
      @ok="save"
      @cancel="resetAddUserParams"
    >
      <a-tabs default-active-key="1">
        <a-tab-pane key="1" :tab="$t('userTabTitleOne')">
          <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
            <a-form-model-item :label="$t('name')">
              <a-input v-model="addUserParams.name" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('userName')">
              <a-input v-model="addUserParams.userName" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('password')">
              <a-input v-model="addUserParams.password" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('email')">
              <a-input v-model="addUserParams.email" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('phoneNumber')">
              <a-input v-model="addUserParams.phoneNumber" size="small" />
            </a-form-model-item>
          </a-form-model>
        </a-tab-pane>
        <a-tab-pane key="2" :tab="$t('userTabTitleTwo')" force-render>
          <a-checkbox-group @change="roleSelectedChange">
            <a-row>
              <a-col :span="14" v-for="(item, index) in roles" :key="index">
                <a-checkbox :value="item.name">
                  {{ item.name }}
                </a-checkbox>
              </a-col>
            </a-row>
          </a-checkbox-group>
        </a-tab-pane>
      </a-tabs>
    </a-modal>
    <!-- 编辑用户 -->
    <a-modal
      :bodyStyle="{ 'padding-top': '0' }"
      v-model="editUserVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="editUserSave"
    >
      <a-tabs default-active-key="1">
        <a-tab-pane key="1" :tab="$t('userTabTitleOne')">
          <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
            <a-form-model-item :label="$t('name')">
              <a-input v-model="editUserParams.name" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('userName')">
              <a-input v-model="editUserParams.userName" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('email')">
              <a-input v-model="editUserParams.email" size="small" />
            </a-form-model-item>
            <a-form-model-item :label="$t('phoneNumber')">
              <a-input v-model="editUserParams.phoneNumber" size="small" />
            </a-form-model-item>
          </a-form-model>
        </a-tab-pane>
        <a-tab-pane key="2" :tab="$t('userTabTitleTwo')" force-render>
          <a-checkbox-group
            @change="roleEditSelectedChange"
            :default-value="editUserParams.roleNames"
          >
            <a-row>
              <a-col :span="14" v-for="(item, index) in roles" :key="index">
                <a-checkbox :value="item.name">
                  {{ item.name }}
                </a-checkbox>
              </a-col>
            </a-row>
          </a-checkbox-group>
        </a-tab-pane>
      </a-tabs>
    </a-modal>
  </div>
</template>

<script>
import {
  getAbpUserList,
  postAbpUser,
  getUserRoleById,
  putAbpUser,
} from "@/services/admin/user";

import { getAbpRoleListAll } from "@/services/admin/role";

export default {
  i18n: require("./i18n"),
  data() {
    return {
      columns: [
        { title: this.$t("name"), dataIndex: "name", key: "name" },
        { title: this.$t("userName"), dataIndex: "userName", key: "userName" },
        { title: this.$t("email"), dataIndex: "email", key: "email" },
        {
          title: this.$t("creationTime"),
          dataIndex: "creationTime",
          key: "creationTime",
        },
        {
          title: this.$t("action"),
          key: "action",
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
        //showTotal: (total) => `共 ${total} 条`, // 显示总数
      },
      createUserVisible: false, // 创建用户模态框是否可见
      editUserVisible: false, // 编辑用户模态框是否可见
      addUserParams: {
        id: "",
        name: "",
        userName: "",
        password: "",
        email: "",
        phoneNumber: "",
        roleNames: [],
      },
      roles: [],
      editUserParams: {
        roleNames: [],
        email: "",
        phoneNumber: "",
        userName: "",
        name: "",
        concurrencyStamp: "",
      },
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.loading = true;
      getAbpUserList(this.queryParam).then((res) => {
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
    resetAddUserParams() {
      this.addUserParams.name = "";
      this.addUserParams.userName = "";
      this.addUserParams.password = "";
      this.addUserParams.email = "";
      this.addUserParams.phoneNumber = "";
      this.addUserParams.roles = [];
      this.createUserVisible = false;
    },
    handleTableChange(pagination) {
      this.pagination.current = pagination.current;
      this.pagination.pageSize = pagination.pageSize;
      this.queryParam.skipCount =
        (pagination.current - 1) * pagination.pageSize;
      this.queryParam.maxResultCount = pagination.pageSize;
      this.getList();
    },
    showAddUserModal() {
      this.createUserVisible = true;
      this.getRoleAll();
    },
    // 获取所有角色
    getRoleAll() {
      getAbpRoleListAll().then((res) => {
        this.roles = res.data.items;
      });
    },
    // 角色选择
    roleSelectedChange(value) {
      this.addUserParams.roleNames = value;
    },
    roleEditSelectedChange(value) {
      this.editUserParams.roleNames = value;
    },
    // 新增用户之前验证参数
    beforeSave(type) {
      if (type == "add") {
        if (!this.addUserParams.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
        if (!this.addUserParams.userName) {
          this.$message.error(this.$t("inputPrefix") + this.$t("userName"));
          return false;
        }
        if (!this.addUserParams.password) {
          this.$message.error(this.$t("inputPrefix") + this.$t("password"));
          return false;
        }
        if (!this.addUserParams.email) {
          this.$message.error(this.$t("inputPrefix") + this.$t("email"));
          return false;
        }

        if (!this.addUserParams.roleNames) {
          this.$message.error(
            this.$t("selectedPrefix") + this.$t("userTabTitleTwo")
          );
          return false;
        }
      } else if (type == "edit") {
        if (!this.editUserParams.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
        if (!this.editUserParams.userName) {
          this.$message.error(this.$t("inputPrefix") + this.$t("userName"));
          return false;
        }
        if (!this.editUserParams.email) {
          this.$message.error(this.$t("inputPrefix") + this.$t("email"));
          return false;
        }

        if (!this.editUserParams.roleNames) {
          this.$message.error(
            this.$t("selectedPrefix") + this.$t("userTabTitleTwo")
          );
          return false;
        }
      }
      return true;
    },
    save() {
      if (!this.beforeSave("add")) return;
      postAbpUser(this.addUserParams).then(() => {
        this.$message.success(this.$t("successMsg"));
        this.resetAddUserParams();
        this.getList();
      });
    },
    editUserMotal(text, row) {
      this.getUserRoleById(row.id);
      this.getRoleAll();
      this.editUserParams.name = row.name;
      this.editUserParams.userName = row.userName;
      this.editUserParams.email = row.email;
      this.editUserParams.phoneNumber = row.phoneNumber;
      this.editUserParams.id = row.id;
      this.editUserParams.concurrencyStamp = row.concurrencyStamp;
    },
    editUserSave() {
      if (!this.beforeSave("edit")) return;
      putAbpUser(this.editUserParams.id, this.editUserParams).then(() => {
        this.editUserVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    getUserRoleById(id) {
      getUserRoleById(id).then((res) => {
        this.editUserParams.roleNames = res.data.items.map((e) => e.name);
        this.editUserVisible = true;
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
  font-size: 12px;
  margin-left: 40px;

  span {
    line-height: 12px;
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
