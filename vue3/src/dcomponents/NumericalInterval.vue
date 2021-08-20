<template>
  <div>
    <a-input v-model:value="rangeInputs[0]" style="width: 142px; text-align: center" placeholder="最小值" @input="inputChange" />
    <a-input v-model:value="interval" style="width: 32px; border-left: 0; pointer-events: none; background-color: #fff" placeholder="~" disabled />
    <a-input v-model:value="rangeInputs[1]" style="width: 142px; text-align: center; border-left: 0" placeholder="最大值" @input="inputChange" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, unref } from 'vue';
export default defineComponent({
  name: 'NumericalInterval',
  props: {
    weight: {
      type: Array,
      default: () => [],
    },
  },
  emits: ['change'],
  setup(props, { emit }) {
    let rangeInputs = ref<any>(props.weight);
    let interval = ref<string>('');
    const inputChange = () => {
      emit('change', unref(rangeInputs));
    };
    return {
      interval,
      inputChange,
      rangeInputs,
    };
  },
});
</script>

<style lang="less" scoped>
</style>
