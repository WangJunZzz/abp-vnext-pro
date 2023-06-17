<template>
  <BasicDrawer v-bind="$attrs" @register="register" :title="t('routes.admin.detail')" width="50%">
    <JsonPreview :data="auditLogInfo" />
  </BasicDrawer>
</template>
<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { JsonPreview } from '/@/components/CodeEditor';
  export default defineComponent({
    components: { BasicDrawer, JsonPreview },
    setup() {
      const { t } = useI18n();
      const auditLogInfo = ref({});
      const [register] = useDrawerInner((data) => {
        auditLogInfo.value = unref(data.record);
      });

      return { t, register, auditLogInfo };
    },
  });
</script>
