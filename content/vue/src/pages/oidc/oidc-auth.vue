<template>
<div></div>
</template>

<script>

import Oidc from "oidc-client";
import { settings } from '../../config/oidc'

export default {
    name: 'oidc-auth',
    data() {
        return {};
    },
    created() {
       this.getUserInfo();
    },
    methods:{
 
        getUserInfo(){
            this.mgr = new Oidc.UserManager(settings);
            this.mgr.getUser().then((user) => {
            let payload={userName:user.profile.name,userEmail:user.profile.email,token:user.access_token}
            this.$store.commit("setUserInfo", payload)
            this.$router.push('/dashboard')
        })
        }
    }
};


</script>