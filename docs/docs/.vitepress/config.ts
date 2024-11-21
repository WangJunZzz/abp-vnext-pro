import { basename } from 'node:path'
import { defineConfig } from 'vitepress'
import MarkdownPreview from 'vite-plugin-markdown-preview'

import { head, nav, sidebar } from './configs'

const APP_BASE_PATH = basename(process.env.GITHUB_REPOSITORY || '')

export default defineConfig({
  outDir: '../dist',
  //base: 'https://abp-vnext-pro.github.io/',

  lang: 'zh-CN',
  title: 'Abp VNext Pro',
  description: '企业级管理系统框架,全新升级，开箱即用，简单高效.',
  head,

  lastUpdated: true,
  cleanUrls: true,

  /* markdown 配置 */
  markdown: {
    lineNumbers: true,
  },

  /* 主题配置 */
  themeConfig: {
    i18nRouting: false,

    logo: '/logo.png',

    nav,
    sidebar,

    /* 右侧大纲配置 */
    outline: {
      level: 'deep',
      label: '目录',
    },

    socialLinks: [{ icon: 'github', link: 'https://github.com/WangJunZzz/abp-vnext-pro' }],

    footer: {
      message: '如有转载或 CV 的请标注本站原文地址',
      copyright: 'Copyright © 2024-wangjunzzz',
    },

    lastUpdated: {
      text: '最后更新于',
      formatOptions: {
        dateStyle: 'short',
        timeStyle: 'medium',
      },
    },

    docFooter: {
      prev: '上一篇',
      next: '下一篇',
    },

    returnToTopLabel: '回到顶部',
    sidebarMenuLabel: '菜单',
    darkModeSwitchLabel: '主题',
    lightModeSwitchTitle: '切换到浅色模式',
    darkModeSwitchTitle: '切换到深色模式',

    /*** 自定义配置 ***/
    // visitor: {
    //   //badgeId: 'maomao1996.vitepress-nav-template',
    // },

    comment: {
      repo: 'WangJunZzz/abp-vnext-pro',
      repoId: 'MDEwOlJlcG9zaXRvcnkzNDI0NTE4NTQ',
      category: 'Announcements',
      categoryId: 'DIC_kwDOFGlmjs4CkdrY',
    },
  },

  vite: {
    plugins: [MarkdownPreview()],
  },
})
