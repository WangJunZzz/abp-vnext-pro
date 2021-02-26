
<template>
<div></div>
</template>

<script>
// oidc 登录回调地址
import Oidc from "oidc-client";


export default {
    name: 'oidc-callback',
    data() {
        return {};
    },
    created() {
        console.log('callback')
        new Oidc.UserManager()
            .signinRedirectCallback()
            .then(() => {
                this.$router.push("/oidc-auth");
            });
    }
};
</script>