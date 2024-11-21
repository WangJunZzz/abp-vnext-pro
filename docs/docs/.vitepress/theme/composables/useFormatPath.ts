import { useData, useRoute } from 'vitepress'
import { computed } from 'vue'

/**
 * 对 route.path 进行格式化，统一 github pages 和其他部署平台的 route.path
 */
export function useFormatPath() {
  const { site } = useData()
  const route = useRoute()

  return computed(() => route.path.replace(site.value.base.replace(/\/$/, ''), ''))
}
