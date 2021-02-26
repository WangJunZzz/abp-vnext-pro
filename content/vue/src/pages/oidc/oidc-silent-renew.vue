<template>
<div></div>
</template>

<script>
// oidc 登录回调地址
import Oidc from "oidc-client";


export default {
    name: 'oidc-silent-renew',
    data() {
        return {};
    },
    created() {
        console.log('oidc-silent-renew')
        this.mgr = new Oidc.UserManager()
            .signinSilentCallback()
            .catch(function (e) {
                console.log("oidc-silent-renew" + e)
            });
    }
};
</script>
