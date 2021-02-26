
const { protocol, hostname, port } = window.location,
currentHost = `${protocol}//${hostname}${port ? `:${port}` : ""}`;
export  const settings = {
    authority: process.env.VUE_APP_AUTH_URL,
    client_id: process.env.VUE_APP_CLIENT_ID,
    redirect_uri: currentHost+'/oidc-callback',
    post_logout_redirect_uri: process.env.VUE_APP_AUTH_URL,
    response_type: `id_token token`,
    scope: 'openid email profile yh.swms.openapi.keke.api',
    silent_redirect_uri:currentHost+'/oidc-silent-renew',
    automaticSilentRenew: true, // If true oidc-client will try to renew your token when it is about to expire
    automaticSilentSignin: true // If true vuex-oidc will try to silently signin unauthenticated users on public routes. Defaults to true
  }
