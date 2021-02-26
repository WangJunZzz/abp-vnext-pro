// 判断操作权限
exports.install = function (Vue) {
    Vue.prototype.isGranted = function (value) {
        const grantPolicy = Object.keys(this.$store.state.account.permissions)
        if (grantPolicy.find(e => e == value)) {
            return true;
        } else {
            return false
        }
    };
};