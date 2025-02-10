<template>
    <PageWrapper v-loading="loading">
        <div ref="wrapperRef" :class="prefixCls">
            <Tabs tab-position="left" :tabBarStyle="tabBarStyle" @change="change">
                <template v-for="item in featureList" :key="item.name">
                    <TabPane :tab="item.displayName">
                        <CollapseContainer :title="item.displayName" :canExpan="false">
                            <a-form :label-col="labelCol" :wrapper-col="wrapperCol">
                                <a-form-item v-for="feature in item.features" :label="feature.displayName">
                                    <div v-if="feature.valueType.name === 'FreeTextStringValueType'">
                                        <a-input v-model:value="feature.value" style="width: 80%" />
                                    </div>

                                    <div v-if="feature.valueType.name === 'ToggleStringValueType'">
                                        <a-checkbox :checked="!(feature.value == 'false' || feature.value == false)"
                                            @update:checked="(val) => (feature.value = val)">
                                        </a-checkbox>
                                    </div>
                                </a-form-item>
                            </a-form>
                            <a-button style="margin-left: 65%" type="primary" @click="update(item)">{{
        t('common.saveText')
                                }}</a-button>
                        </CollapseContainer>
                    </TabPane>
                </template>
            </Tabs>
        </div>
    </PageWrapper>

</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted } from 'vue';
import { useI18n } from '/@/hooks/web/useI18n';
import { Tabs } from 'ant-design-vue';
import { CollapseContainer, ScrollContainer } from '/@/components/Container/index';
import { PageWrapper } from '/@/components/Page';
import { getHostFeatureListAsync, updateHostFeatureListAsync } from '/@/views/admin/manageFeatrue/index';
import { FeatureGroupDto, UpdateFeatureDto } from '/@/services/ServiceProxies';
import { message } from 'ant-design-vue';
export default defineComponent({
    name: 'ManageHostFeatrue',
    components: {
        ScrollContainer,
        CollapseContainer,
        Tabs,
        TabPane: Tabs.TabPane,
        PageWrapper,
    },
    setup() {
        const { t } = useI18n();
        let featureList: FeatureGroupDto[] = [];
        const state = reactive({
            featureList,
            loading: true,
        });
        onMounted(async () => {
            state.loading = true;
            const result = await getHostFeatureListAsync();
            state.featureList = result.groups as FeatureGroupDto[];
            state.loading = false;
        });

        const update = async (item) => {
            console.log(item)
            let features = item.features?.map(e => {
                let item = new UpdateFeatureDto();
                item.name = e.name;
                item.value = e.value?.toString();
                return item;
            })

            try {
                state.loading = true;
                await updateHostFeatureListAsync(features)
                message.success(t('common.operationSuccess'));
            } catch (error) {
                message.success(t('common.operationFail'));
            } finally {
                state.loading = false;
            }
        }
        const submit = async () => {

            if (currentFeature) {
                let features = currentFeature.features?.map(e => {
                    let item = new UpdateFeatureDto();
                    item.name = e.name;
                    item.value = e.value?.toString();
                    return item;
                })

                try {
                    state.loading = true;
                    await updateHostFeatureListAsync(features)
                    message.success(t('common.operationSuccess'));
                } catch (error) {
                    message.success(t('common.operationFail'));
                } finally {
                    state.loading = false;
                }

            }
        };
        const cancel = () => {
        };
        return {
            t,
            submit,
            cancel,
            ...toRefs(state),
            prefixCls: 'account-setting',
            tabBarStyle: {
                width: '220px',
            },
            labelCol: { span: 4 },
            wrapperCol: { span: 14 },
            update
        };
    },
});
</script>
<style lang="less">
.account-setting {
    margin: 12px;
    background-color: @component-background;

    .base-title {
        padding-left: 0;
    }

    .ant-tabs-tab-active {
        background-color: @item-active-bg;
    }
}
</style>