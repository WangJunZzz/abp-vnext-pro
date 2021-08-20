import type { ProjectConfig } from '/#/config';
import { MenuTypeEnum, MenuModeEnum, TriggerEnum, MixSidebarTriggerEnum } from '/@/enums/menuEnum';
import { CacheTypeEnum } from '/@/enums/cacheEnum';
import {
  ContentEnum,
  PermissionModeEnum,
  ThemeEnum,
  RouterTransitionEnum,
  SettingButtonPositionEnum,
  SessionTimeoutProcessingEnum,
} from '/@/enums/appEnum';
import { SIDE_BAR_BG_COLOR_LIST, HEADER_PRESET_BG_COLOR_LIST } from './designSetting';
import { primaryColor } from '../../build/config/themeConfig';

// ! You need to clear the browser cache after the change
const setting: ProjectConfig = {
  showSettingButton: true,
  showDarkModeToggle: true,
  settingButtonPosition: 'auto',
  sessionTimeoutProcessing: 1,
  permissionMode: 'ROLE',
  permissionCacheType: 1,
  themeColor: '#0960bd',
  grayMode: false,
  colorWeak: false,
  fullContent: false,
  contentMode: 'full',
  showLogo: true,
  showFooter: false,
  headerSetting: {
    bgColor: '#151515',
    fixed: true,
    show: true,
    theme: 'dark',
    useLockPage: true,
    showFullScreen: true,
    showDoc: true,
    showNotice: true,
    showSearch: true,
  },
  menuSetting: {
    bgColor: '#001529',
    fixed: true,
    collapsed: false,
    collapsedShowTitle: false,
    canDrag: false,
    show: true,
    hidden: false,
    menuWidth: 210,
    mode: 'inline',
    type: 'sidebar',
    theme: 'dark',
    split: false,
    topMenuAlign: 'start',
    trigger: 'HEADER',
    accordion: true,
    closeMixSidebarOnChange: false,
    mixSideTrigger: 'click',
    mixSideFixed: false,
  },
  multiTabsSetting: {
    cache: false,
    show: true,
    canDrag: true,
    showQuick: true,
    showRedo: true,
    showFold: true,
  },
  transitionSetting: {
    enable: false,
    basicTransition: 'fade-slide',
    openPageLoading: true,
    openNProgress: false,
  },
  openKeepAlive: true,
  lockTime: 0,
  showBreadCrumb: true,
  showBreadCrumbIcon: false,
  useErrorHandle: false,
  useOpenBackTop: true,
  canEmbedIFramePage: true,
  closeMessageOnSwitch: true,
  removeAllHttpPending: false,
};

export default setting;
