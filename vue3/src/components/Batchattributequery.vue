<template>
  <BasicModal @register="reinnergister" v-bind="$attrs" width="800px" title="批属性查询">
    <!-- <a-card title="批属性查询" style="width: 100% ;"> -->
    <a-form layout="inline" :model="formState" :label-col="labelCol" :wrapper-col="wrapperCol">
      <a-form-item label="生产日期">
        <a-date-picker v-model:value="formState.ProductionDate" format="YYYY-MM-DD HH:mm:ss" show-time placeholder="请输入生产日期" />
      </a-form-item>
      <a-form-item label="失效日期">
        <a-date-picker v-model:value="formState.ExpireDate" format="YYYY-MM-DD HH:mm:ss" show-time placeholder="请输入失效日期" />
      </a-form-item>
      <a-form-item label="入库日期">
        <a-date-picker v-model:value="formState.EntryDate" format="YYYY-MM-DD HH:mm:ss" show-time placeholder="请输入入库日期" />
      </a-form-item>
      <a-form-item label="Plant">
        <a-input v-model:value="formState.Plant" placeholder="请输入Plant"> </a-input>
      </a-form-item>
      <a-form-item label="生产批次">
        <a-input v-model:value="formState.ProductionBatch" placeholder="请输入生产批次"> </a-input>
      </a-form-item>
      <!-- ProductionBatch -->
      <a-form-item label="箱号">
        <a-input v-model:value="formState.BoxNo" placeholder="请输入箱号"> </a-input>
      </a-form-item>
      <a-form-item label="批次编码">
        <a-input v-model:value="formState.BatchCode" placeholder="请输入批次编码"> </a-input>
      </a-form-item>
      <a-form-item label="退货原因">
        <a-input v-model:value="formState.ReturnReason" placeholder="请输入退货原因"> </a-input>
      </a-form-item>
      <a-form-item label="虚仓">
        <a-input v-model:value="formState.VirtualWarehouse" placeholder="请输入虚仓"> </a-input>
      </a-form-item>
      <a-form-item label="箱规">
        <a-input v-model:value="formState.BoxQty" placeholder="请输入箱规"> </a-input>
      </a-form-item>
      <a-form-item label="PO">
        <a-input v-model:value="formState.PO" placeholder="请输入PO"> </a-input>
      </a-form-item>
    </a-form>
    <template #footer>
      <a-button key="submit" type="primary" :disabled="!isclick">查询</a-button>
      <a-button key="back" @click="cancel">取消</a-button>
    </template>
    <!-- </a-card> -->
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, UnwrapRef, reactive, computed } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
interface FormState {
  ProductionDate: string;
  ExpireDate: string;
  EntryDate: string;
  ProductionBatch: string;
  Plant: string;
  BoxNo: string;
  BatchCode: string;
  ReturnReason: string;
  VirtualWarehouse: string;
  BoxQty: string;
  PO: string;
}
type projectkey = {
  [P in keyof FormState]?:boolean
}
export default defineComponent({
  props: {
    Batchattribute: {
      type: Object as PropType<FormState>,
      default:() => {},
      // required: true,
    },
  },
  setup(props) {
    const formState: UnwrapRef<FormState> = reactive({
      ProductionDate: '',
      ExpireDate: '',
      EntryDate: '',
      ProductionBatch: '',
      Plant: '',
      BoxNo: '',
      BatchCode: '',
      ReturnReason: '',
      VirtualWarehouse: '',
      BoxQty: '',
      PO: '',
    });
    const [reinnergister, { closeModal }] = useModalInner();
    const cancel = () => {
      closeModal();
    };
    // 批属性显示
    const projectkey = computed(() => {
      let obj:projectkey = {};
      if(typeof props.Batchattribute !== 'object') return obj
      let Batchattribute = Object.keys(props.Batchattribute) ;
      if(Batchattribute.length === 0) return obj
      Batchattribute.forEach((item) => {
        obj[item] = true;
      });
      return obj;
    });
    // 存在输入，按钮可点击
    const isclick = computed(() => {
      if(!props.Batchattribute) return false
      return Object.values(props.Batchattribute).some((item) => {
        return item !== '' && item !== null;
      });
    });
    return {
      formState,
      labelCol: { style: { width: '150px' } },
      wrapperCol: { span: 13 },
      isclick,
      reinnergister,
      cancel,
      projectkey,
    };
  },
  components: {
    BasicModal,
  },
});
</script>

<style>
</style>
