<template>
  <BasicModal v-bind="$attrs" :canFullscreen="false" :title="getTitle" :footer="null" :destroyOnClose="true" :maskClosable="false">
    <div class="modelwrap">
      <div class="wrap">
        <div class="item">
          <p class="item-title">下载模板:</p>
          <a-button class="item-btn" type="primary" @click="exportTask">点击下载模板</a-button>
        </div>
        <div class="item">
          <p class="item-title">选择文件:</p>
          <a-upload
            v-model:file-list="fileList"
            name="file"
            accept=".xls .xlsx .csv"
            :multiple="false"
            action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
            :headers="headers"
            @change="handleChange"
            :before-upload="beforeUpload"
            :customRequest="customRequest"
          >
            <a-button>
              <upload-outlined></upload-outlined>
              上传文件
            </a-button>
          </a-upload>
        </div>
        <p class="desc">支持扩展名：.xls .xlsx .csv</p>
      </div>
      <div class="empty"></div>
      <div class="foot">
        <a-button class="foot-item" type="primary" @click="save">导入</a-button>
        <a-button class="foot-item" @click="cancel">取消</a-button>
      </div>
    </div>
  </BasicModal>
</template>
<script lang="ts">
import { defineComponent, onMounted, ref, computed } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { Icon } from '/@/components/Icon';
import { importAsync } from './oss';
import { message } from 'ant-design-vue';
import { UploadOutlined } from '@ant-design/icons-vue';
import { AliYunServiceProxy, ImportTaskInput } from '/@/services/ServiceProxies';
import moment from 'moment';
import OSS from 'ali-oss';
interface FileItem {
  uid: string;
  name?: string;
  status?: string;
  response?: string;
  url?: string;
  type?: string;
  size: number;
  originFileObj: any;
}
interface FileInfo {
  file: FileItem;
  fileList: FileItem[];
}
// interface Ossset {
//   SecurityToken:string,
//   accessKeySecret:string,
//   token:string,
//   expiration:string
// }

export default defineComponent({
  name: 'UploadOss',
  components: { BasicModal, Icon, UploadOutlined },
  props: {
    title: String,
    importType: Number,
  },

  emits: ['close', 'save'],
  setup(props, { emit }) {
    let client = ref({});
    let fileList = ref([]);
    let data: any = {};
    const [registerModal] = useModalInner();
    const _AliYunServiceProxy = new AliYunServiceProxy();
    const getTitle = computed(() => {
      return props.title;
    });
    const getTemporaryCredentials = async () => {
      return await _AliYunServiceProxy.getTemporaryCredentials();
    };
    const cancel = () => {
      emit('close');
      fileList.value = [];
    };
    onMounted(() => {
      getTemporaryCredentials().then((res) => {
        let params = {
          SecurityToken: res.token,
          AccessKeySecret: res.accessKeySecret,
          AccessKeyId: res.accessKeyId,
          Expiration: res.expiration,
        };
        // ossParameter = params;
        client.value = new OSS({
          region: 'oss-cn-shenzhen',
          accessKeyId: params.AccessKeyId as string,
          accessKeySecret: params.AccessKeySecret as string,
          bucket: 'yhglobal-asdf',
          stsToken: params.SecurityToken,
        });
      });
    });
    // function customRequest(action) {
    //   // let prev = moment().format('YYYY-MM-DD')
    //   (client.value as OSS)
    //     .put(`yhwms/${moment().format('YYYY_MM_DD')}/${action.file.name}`, action.file)
    //     .then(function (res) {
    //       console.log(11111111111111111111, res);
    //       console.log(fileList, fileList);
    //       action.onSuccess(res, action);
    //     })
    //     .catch(() => {
    //       action.onError();
    //     });
    // }

    function customRequest(action) {
      let [name, type] = action.file.name?.split('.');
      (client.value as OSS)
        .put(`yhwms/${moment().format('YYYY_MM_DD')}/${name}_${moment().format('X')}.${type}`, action.file)
        .then(function (res) {
          data = res;
          action.onSuccess(res, action);
        })
        .catch(() => {
          action.onError();
        });
    }
    const success = () => {
      message.success('This is a success message');
    };
    const error = () => {
      message.error('This is an error message');
    };
    const warning = () => {
      message.warning('This is a warning message');
    };
    const handleChange = (info: FileInfo) => {
      fileList.value = fileList.value.filter((item: FileItem) => {
        let type = item!.name?.split('.')[1];
        return type === 'xls' || type === 'xlsx' || type === 'csv';
      });
      console.log(info);
      // if (info.file.status !== 'uploading') {
      //   console.log(info.file, info.fileList);
      // }
      // if (info.file.status === 'done') {
      //   message.success(`${info.file.name} file uploaded successfully`);
      // } else if (info.file.status === 'error') {
      //   message.error(`${info.file.name} file upload failed.`);
      // }
    };
    const beforeUpload = (file: FileItem) => {
      const type = file.name?.split('.')[1];
      const isJpgOrPng = type === 'xls' || type === 'xlsx' || type === 'csv';
      if (!isJpgOrPng) {
        message.error('请上传.xls .xlsx .csv格式文件');
      }
      return isJpgOrPng;
    };

    const save = async () => {
      if (fileList.value.length == 0) {
        message.error('请上传文件再进行此操作');
        return;
      } else {
        let request = new ImportTaskInput();
        request.ossFileName = data.name;
        request.taskType = props.importType as number;
        await importAsync(request);
        message.success('导入商品成功');
        emit('close');
        fileList.value = [];
      }
    };
    const exportTask = async () => {
      emit('save');
      emit('close');
    };
    return {
      cancel,
      success,
      error,
      warning,
      fileList,
      headers: {
        authorization: 'authorization-text',
      },
      handleChange,
      beforeUpload,
      customRequest,
      save,
      getTitle,
      registerModal,
      exportTask,
    };
  },
});
</script>
<style lang="less" scoped>
.desc {
  // color: rgb(240,242,245);
  margin-left: 69px;
}

.modelwrap {
  display: flex;
  height: 100%;
  flex-direction: column;
  justify-content: space-around;
}

.item {
  display: flex;
  margin-top: 20px;
}

.item-title {
  margin-right: 10px;
}

.foot {
  text-align: right;

  .foot-item {
    margin-left: 8px;
  }
}
</style>
