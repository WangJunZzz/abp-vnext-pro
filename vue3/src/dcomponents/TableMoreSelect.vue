<template>
  <Select
    v-bind="attrs"
    :options="getOptions"
    v-model:value="state"
    @search="handleFetch"
    @inputKeydown="inputKeydown"
    :dropdownMatchSelectWidth="false"
    :filterOption="() => true"
  >
    <template #dropdownRender="{ menuNode }">
      <div class="select-table-container" @mousedown="(e) => e.preventDefault()">
        <div style="position: absolute; height: 0; overflow: hidden ;">
          <v-nodes :vnodes="menuNode" />
        </div>
        <div class="header"></div>
        <div class="content">
          <a-table
            class="result-table"
            bordered
            size="small"
            :columns="columns"
            :loading="loading"
            :data-source="options"
            :pagination="pagination"
            :row-key="rowKey || valueField"
            :custom-row="
              (record, index) => {
                return {
                  onClick: () => {
                    clickRow(menuNode, index);
                  },
                };
              }
            "
            :rowClassName="rowClassName"
            @change="handleTableChange"
          ></a-table>
        </div>
      </div>
    </template>
    <template #suffixIcon v-if="loading">
      <LoadingOutlined spin />
    </template>
    <template #notFoundContent v-if="loading">
      <span>
        <LoadingOutlined spin class="mr-1" />
        {{ t('component.form.tableSelectNotFound') }}
      </span>
    </template>
  </Select>
</template>
<script lang="ts">
import { defineComponent, PropType, ref, watchEffect, computed, unref, reactive } from 'vue';
import { Select } from 'ant-design-vue';
import { isFunction } from '/@/utils/is';
import { useRuleFormItem } from '/@/hooks/component/useFormItem';
import { useAttrs } from '/@/hooks/core/useAttrs';
import { get } from 'lodash-es';

import { LoadingOutlined } from '@ant-design/icons-vue';
import { useI18n } from '/@/hooks/web/useI18n';
import { propTypes } from '/@/utils/propTypes';
import debounce from 'lodash/debounce';

type OptionsItem = { label: string; value: string; disabled?: boolean };
type Pagination = {
  pageSize: number;
  showLessItems?: boolean;
  showTotal?: Function;
  current: number;
  total: number;
};

export default defineComponent({
  name: 'TableMoreSelect',
  components: {
    Select,
    LoadingOutlined,
    VNodes: (_, { attrs }) => {
      return attrs.vnodes;
    },
  },
  inheritAttrs: false,
  props: {
    value: propTypes.oneOfType([
      propTypes.object,
      propTypes.number,
      propTypes.string,
      propTypes.array,
    ]),
    numberToString: propTypes.bool,
    api: {
      type: Function as PropType<(arg?: Recordable) => Promise<OptionsItem[]>>,
      default: null,
    },
    // api params
    params: {
      type: Object as PropType<Recordable>,
      default: () => {},
    },
    // support xxx.xxx.xx
    resultField: propTypes.string.def('items'),
    labelField: propTypes.string.def('label'),
    valueField: propTypes.string.def('value'),
    immediate: propTypes.bool.def(true),
    columns: propTypes.array.def([]),
    rowKey: propTypes.string.def(''),
    debounceDelay: propTypes.number.def(500),
    keyWordField: propTypes.string.def('keyWord'),
    pageSizeField: propTypes.string.def('pageSize'),
    currentField: propTypes.string.def('pageIndex'),
    totalCountField: propTypes.string.def('totalCount'),
  },
  emits: ['options-change', 'change', 'table-change'],
  setup(props, { emit }) {
    const options = ref<OptionsItem[]>([]);
    const loading = ref(false);
    const isFirstLoad = ref(true);
    const attrs = useAttrs();
    const { t } = useI18n();

    // Embedded in the form, just use the hook binding to perform form verification
    const [state] = useRuleFormItem(props);
    /**
     * @description: valueSet 用来判断表格行是否选中状态
     * @param {*} computed
     * @return {*}
     */
    const valueSet = computed(() => {
      return new Set((state as any)?.value || []);
    });
    let pagination = reactive(<Pagination>{
      pageSize: 10,
      showLessItems: true,
      showTotal: (total: number) => `共 ${total} 条`,
      current: 1,
      total: 0,
    });
    let keyWord = ref('');

    const getOptions = computed(() => {
      const { labelField, valueField, numberToString } = props;

      return unref(options).reduce((prev, next: Recordable) => {
        if (next) {
          const value = next[valueField];
          prev.push({
            label: next[labelField],
            value: numberToString ? `${value}` : value,
          });
        }
        return prev;
      }, [] as OptionsItem[]);
    });

    watchEffect(() => {
      props.immediate && fetch();
    });

    async function fetch() {
      const api = props.api;
      if (!api || !isFunction(api)) return;

      const { keyWordField, currentField, pageSizeField } = props;
      const params = {
        [`${keyWordField}`]: keyWord.value,
        [`${currentField}`]: pagination.current,
        [`${pageSizeField}`]: pagination.pageSize,
      };
      //console.log(params);
      try {
        loading.value = true;
        // const res = await api(props.params);
        const res = await api(params);
        //如果响应是数组,直接赋值options,如不是,取resultField指定的字段
        if (Array.isArray(res)) {
          options.value = res;
          emitChange();
          return;
        }
        if (props.resultField) {
          options.value = get(res, props.resultField) || [];
        }
        pagination.total = get(res, props.totalCountField);
        emitChange();
      } catch (error) {
        console.warn(error);
      } finally {
        loading.value = false;
      }
    }

    let handleFetch = debounce(async (val: string) => {
      keyWord.value = val;
      if (!props.immediate && unref(isFirstLoad)) {
        pagination.current = 1;
        await fetch();
        isFirstLoad.value = false;
      }
    }, props.debounceDelay);

    function inputKeydown(e) {
      console.log(e);
    }

    function emitChange() {
      emit('options-change', unref(options));
    }

    async function handleTableChange(paginationData: Pagination) {
      pagination.current = paginationData.current;
      pagination.pageSize = paginationData.pageSize;
      await fetch();
      emit('table-change', paginationData);
    }
    function clickRow(menuNode, index: number) {
      // menuNode?.elm?.children[0]?.children[index]?.click();
      try {
        menuNode.el.parentNode
          .querySelectorAll('.ant-select-item.ant-select-item-option')
          [index].click();
      } catch (error) {
        console.error('找不到元素');
      }
    }
    /**
     * @description: 设置行类名
     * @param {*} record 行对象
     * @return {*} 类名
     */
    function rowClassName(record): string {
      if(valueSet.value.has(record[props.valueField])){
        return "row-checked"
      }
      return ""
    }
    return {
      state,
      attrs,
      valueSet,
      options,
      getOptions,
      loading,
      pagination,
      t,
      handleFetch,
      inputKeydown,
      handleTableChange,
      clickRow,
      rowClassName,
    };
  },
});
</script>
<style lang="less" scoped>
.select-table-container {
  padding: 0 5px;
}
</style>
<style lang="less">
.select-table-container {
  padding: 0 5px;

  .row-checked {
    background-color: #f5f5f5;
  }

}
</style>
