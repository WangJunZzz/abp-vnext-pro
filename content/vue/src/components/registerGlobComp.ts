import Icon from './Icon/index';
import { Button } from './Button';
import {
  // Need
  Button as AntButton,
  Checkbox as AntCheckbox
} from 'ant-design-vue';

import { App } from 'vue';

const compList = [Icon, Button, AntButton.Group, AntCheckbox, AntCheckbox.Group];
import Antd from 'ant-design-vue'

export function registerGlobComp(app: App) {
  compList.forEach((comp: any) => {
    app.component(comp.name || comp.displayName, comp);
    // 注册全部
    app.use(Antd)
  });
}
