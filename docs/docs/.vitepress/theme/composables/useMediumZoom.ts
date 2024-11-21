import type { Router } from 'vitepress'
import type { App, InjectionKey } from 'vue'
import type { Zoom } from 'medium-zoom'

import { inject, watch } from 'vue'
import mediumZoom from 'medium-zoom'

declare module 'medium-zoom' {
  interface Zoom {
    refresh: (selector?: string) => void
  }
}

const defaultSelector = ':not(a) > img:not(.image-src, .visitor, .vp-sponsor-grid-image)'

export const mediumZoomSymbol: InjectionKey<Zoom> = Symbol('medium-zoom')

export const createMediumZoomProvider = (app: App, router: Router) => {
  if (import.meta.env.SSR) {
    return
  }

  const zoom = mediumZoom()
  // 扩展 zoom.refresh 方法
  zoom.refresh = (selector = defaultSelector) => {
    zoom.detach()
    zoom.attach(selector)
  }

  app.provide(mediumZoomSymbol, zoom)

  watch(
    () => router.route.path,
    // 使用 nextTick 时在 dev 环境下第一次进入页面无法触发
    () => setTimeout(() => zoom.refresh()),
  )
}

export function useMediumZoom(): Zoom | null {
  if (import.meta.env.SSR) {
    return null
  }

  const zoom = inject(mediumZoomSymbol)
  if (!zoom) {
    throw new Error('useMediumZoom() is called without provider.')
  }
  return zoom
}
