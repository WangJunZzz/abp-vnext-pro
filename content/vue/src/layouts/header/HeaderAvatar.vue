<template>
  <div>
    <a-dropdown>
      <div class="header-avatar" style="cursor: pointer">
        <a-avatar
          class="avatar"
          size="small"
          shape="circle"
          :src="user.avatar"
        />
        <span class="name">{{ user.name }}</span>
      </div>
      <a-menu :class="['avatar-menu']" slot="overlay">
        <a-menu-item @click="changePasswordModal()">
          <a-icon type="user" />
          <span>{{ $t("changePassword") }}</span>
        </a-menu-item>
        <a-menu-divider />
        <a-menu-item @click="logout">
          <a-icon style="margin-right: 8px;" type="poweroff" />
          <span>{{ $t("logout") }}</span>
        </a-menu-item>
      </a-menu>
    </a-dropdown>
    <!-- 修改密码 -->
    <a-modal
      v-model="editChangePasswordVisible"
      :title="$t('changePassword')"
      :destroyOnClose="true"
      @ok="editChangePassword()"
    >
      <a-form-model :label-col="{ span: 5 }" :wrapper-col="{ span: 12 }">
        <a-form-model-item :label="$t('currentPassword')">
          <a-input v-model="editPasswordParams.currentPassword" />
        </a-form-model-item>
        <a-form-model-item :label="$t('newPassword')">
          <a-input v-model="editPasswordParams.newPassword" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>
  </div>
</template>

<script>
import { changePassword } from "@/services/admin/user";
export default {
  i18n: require("./i18n"),
  name: "HeaderAvatar",
  data() {
    return {
      user: {
        avatar:
          "https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png",
        name: "",
      },
      editChangePasswordVisible: false,
      editPasswordParams: {
        currentPassword: "",
        newPassword: "",
      },
    };
  },
  created() {
    this.user.name = this.$store.state.account.userName;
  },
  methods: {
    logout() {
      this.$store.commit("clear");
      this.$router.push("/login");
    },
    beforeChangePassword() {
      if (this.editChangePasswordVisible.currentPassword) {
        this.$message.error(
          this.$t("inputPrefix") + this.$t("currentPassword")
        );
        return false;
      }
      if (this.editChangePasswordVisible.newPassword) {
        this.$message.error(this.$t("inputPrefix") + this.$t("newPassword"));
        return false;
      }
      return true;
    },
    changePasswordModal() {
      debugger;
      this.editChangePasswordVisible = true;
      this.editPasswordParams.currentPassword = "";
      this.editPasswordParams.newPassword = "";
    },
    editChangePassword() {
      if (!this.beforeChangePassword()) return;
      changePassword(this.editPasswordParams).then(() => {
        this.editChangePasswordVisible = false;
        this.editPasswordParams.currentPassword = "";
        this.editPasswordParams.newPassword = "";
        this.$message.success(this.$t("successMsg"));
      });
    },
  },
};
</script>

<style lang="less">
.header-avatar {
  display: inline-flex;

  .avatar,
  .name {
    align-self: center;
  }

  .avatar {
    margin-right: 8px;
  }

  .name {
    font-weight: 500;
  }
}

.avatar-menu {
  width: 150px;
}
</style>
