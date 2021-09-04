<template>
  <div :class="prefixCls">
    <Popover title="" trigger="click" :overlayClassName="`${prefixCls}__overlay`">
      <Badge dot :numberStyle="numberStyle">
        <BellOutlined />
      </Badge>
      <template #content>
        <Tabs>
          <template v-for="item in listData" :key="item.key">
            <TabPane>
              <template #tab>
                {{ item.name }}
              </template>
              <!-- 绑定title-click事件的通知列表中标题是“可点击”的-->
              <NoticeList :list="item.list" v-if="item.key === '1'" />
              <NoticeList :list="item.list" v-else />
            </TabPane>
          </template>
        </Tabs>
      </template>
    </Popover>
  </div>
</template>
<script lang="ts">
  import { defineComponent, computed } from 'vue';
  import { Popover, Tabs, Badge } from 'ant-design-vue';
  import { BellOutlined } from '@ant-design/icons-vue';
  import { PagingNotificationListOutput } from '/@/services/ServiceProxies';
  import { tabListData } from './data';
  import NoticeList from './NoticeList.vue';
  import { useDesign } from '/@/hooks/web/useDesign';
  import { string } from 'vue-types';

  export default defineComponent({
    components: { Popover, BellOutlined, Tabs, TabPane: Tabs.TabPane, Badge, NoticeList },
    props: {
      textMessage: Array as PropType<PagingNotificationListOutput[]>,
      broadCastMessage: Array as PropType<PagingNotificationListOutput[]>,
      ff: string,
    },
    setup(props) {
      const { prefixCls } = useDesign('header-notify');
      const listData = computed(() => {
        tabListData[0].list = props.textMessage;
        tabListData[1].list = props.broadCastMessage;
        return tabListData;
      });
      return {
        prefixCls,
        listData,
        numberStyle: {},
      };
    },
  });
</script>
<style lang="less">
  @prefix-cls: ~'@{namespace}-header-notify';

  .@{prefix-cls} {
    padding-top: 2px;

    &__overlay {
      width: 320px;
    }

    .ant-tabs-content {
      width: 320px;
    }

    .ant-badge {
      font-size: 18px;

      .ant-badge-multiple-words {
        padding: 0 4px;
      }

      svg {
        width: 0.9em;
      }
    }
  }
</style>
