import type { App } from 'vue';
// import { Icon } from './Icon';

const compList: any[] = [];

export function registerGlobComp(app: App) {
  compList.forEach((comp) => {
    app.component(comp.name || comp.displayName, comp);
  });
}
