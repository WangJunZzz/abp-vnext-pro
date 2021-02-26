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
          @click="showAddRoleModal"
          size="small"
          v-if="isGranted('AbpIdentity.Roles.Create')"
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
            v-if="isGranted('AbpIdentity.Roles.Update')"
            @click="editRoleModal(text, record)"
            >{{ $t("edit") }}</a
          >
          <a
            style="margin-left:10px"
            href="javascript:void(0)"
            v-if="isGranted('AbpIdentity.Roles.ManagePermissions')"
            @click="permissionMotal(text, record)"
            >{{ $t("grant") }}</a
          >
        </span>
      </a-table>
    </a-card>

    <!-- 新增角色 -->
    <a-modal
      v-model="addRoleVisible"
      :title="$t('add')"
      :destroyOnClose="true"
      @ok="addRoleSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="addRoleParam.name" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('property')">
          <a-checkbox v-model="addRoleParam.isDefault">
            {{ $t("isDefault") }}
          </a-checkbox>
          <a-checkbox v-model="addRoleParam.isPublic">
            {{ $t("isPublic") }}
          </a-checkbox>
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 编辑角色 -->
    <a-modal
      v-model="editRoleVisible"
      :title="$t('edit')"
      :destroyOnClose="true"
      @ok="editRoleSave"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('name')">
          <a-input v-model="editRoleParam.name" size="small" />
        </a-form-model-item>
        <a-form-model-item :label="$t('property')">
          <a-checkbox name="isDefault" v-model="editRoleParam.isDefault">
            {{ $t("isDefault") }}
          </a-checkbox>
          <a-checkbox name="isPublic" v-model="editRoleParam.isPublic">
            {{ $t("isPublic") }}
          </a-checkbox>
        </a-form-model-item>
      </a-form-model>
    </a-modal>

    <!-- 授权 -->
    <a-drawer
      :title="$t('grant')"
      placement="right"
      :visible="permissionVisible"
      :destroyOnClose="true"
      :closable="false"
      width="320"
    >
      <a-tree checkable :tree-data="treeData" v-model="selectedPermission">
      </a-tree>
      <div
        :style="{
          position: 'absolute',
          right: 0,
          bottom: 0,
          width: '100%',
          borderTop: '1px solid #e9e9e9',
          padding: '10px 16px',
          background: '#fff',
          textAlign: 'right',
          zIndex: 1,
        }"
      >
        <a-button :style="{ marginRight: '8px' }" @click="permissionCancel">
          {{ $t("cancel") }}
        </a-button>
        <a-button type="primary" @click="permisstionConfirm">
          {{ $t("confirm") }}
        </a-button>
      </div>
    </a-drawer>
  </div>
</template>

<script>
import {
  getAbpRoleList,
  getAbpRolePermission,
  putAbpRolePermission,
  postAbpRole,
  putAbpRole,
} from "@/services/admin/role";

export default {
  i18n: require("./i18n"),
  data() {
    return {
      columns: [
        { title: this.$t("name"), dataIndex: "name", key: "name" },
        {
          title: this.$t("isDefault"),
          dataIndex: "isDefault",
          key: "isDefault",
          customRender: function(text) {
            if (text == false) {
              return "False";
            } else {
              return "Ture";
            }
          },
        },
        {
          title: this.$t("isPublic"),
          dataIndex: "isPublic",
          key: "isPublic",
          customRender: function(text) {
            if (text == false) {
              return "False";
            } else {
              return "Ture";
            }
          },
        },
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
      addRoleParam: {
        name: "",
        isDefault: false,
        isPublic: false,
      },
      editRoleParam: {
        id: "",
        name: "",
        isDefault: false,
        isPublic: false,
        concurrencyStamp: "",
      },
      pagination: {
        total: 0,
        pageSize: 10, // 默认每页显示数量
        showSizeChanger: true, // 显示可改变每页数量
        pageSizeOptions: ["10", "20", "30", "40"], // 每页数量选项
        //showTotal: (total) => `共 ${total} 条`, // 显示总数
      },
      permissionVisible: false,
      editRoleName: "", //编辑的时候选中的角色名称
      treeData: [],
      selectedPermission: [],
      addRoleVisible: false,
      editRoleVisible: false,
    };
  },
  created() {
    this.getList();
  },
  watch: {
    selectedPermission() {},
  },
  methods: {
    getList() {
      this.loading = true;
      getAbpRoleList(this.queryParam).then((res) => {
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
    permissionMotal(text, row) {
      this.permissionVisible = true;
      this.editRoleName = row.name;
      this.getRolePermissionByName();
    },
    permissionCancel() {
      this.permissionVisible = false;
      this.selectedPermission = [];
    },
    permisstionConfirm() {
      let putParams = {
        permissions: [],
      };
      this.selectedPermission.forEach((e) => {
        if (e.indexOf(".") > 0) {
          // 接口不需要上传分组的权限
          putParams.permissions.push({
            name: e,
            isGranted: true,
          });
        }
      });
      putAbpRolePermission(this.editRoleName, putParams).then(() => {
        this.permissionVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    getRolePermissionByName() {
      let query = {
        providerName: "R",
        providerKey: this.editRoleName,
      };
      getAbpRolePermission(query).then((res) => {
        let result = [];
        this.selectedPermission = [];
        let list = res.data.groups;
        list.forEach((item) => {
          let temp = {
            title: item.displayName,
            key: item.name,
            children: [],
          };
          item.permissions.forEach((perItem) => {
            temp.children.push({
              title: perItem.displayName,
              key: perItem.name,
            });
            if (perItem.isGranted) {
              this.selectedPermission.push(perItem.name);
            }
          });
          result.push(temp);
        });
        this.treeData = result;
      });
    },

    showAddRoleModal() {
      this.addRoleVisible = true;
    },
    //新增角色
    addRoleSave() {
      if (!this.beforeSave("add")) return;
      postAbpRole(this.addRoleParam).then(() => {
        this.addRoleVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    editRoleModal(text, row) {
      this.editRoleVisible = true;
      this.editRoleParam.id = row.id;
      this.editRoleParam.name = row.name;
      this.editRoleParam.isDefault = row.isDefault;
      this.editRoleParam.isPublic = row.isPublic;
      this.editRoleParam.concurrencyStamp = row.concurrencyStamp;
    },
    //新增角色
    editRoleSave() {
      if (!this.beforeSave("edit")) return;
      putAbpRole(this.editRoleParam.id, this.editRoleParam).then(() => {
        this.editRoleVisible = false;
        this.$message.success(this.$t("successMsg"));
        this.getList();
      });
    },
    beforeSave(type) {
      if (type == "add") {
        if (!this.addRoleParam.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      } else if (type == "edit") {
        if (!this.editRoleParam.name) {
          this.$message.error(this.$t("inputPrefix") + this.$t("name"));
          return false;
        }
      }
      return true;
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
