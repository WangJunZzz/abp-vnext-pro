import Schema from 'async-validator'
import { reactive } from 'vue'
export const usevalidate = (rules) => {
  let state = reactive({
    errorList: {}
  })
  const validate = (value: any[]) => {
    console.log(rules)
    // 没有数据 或者 没有校检, 则直接返回 resolve
    if (rules || !value.length) return Promise.resolve()

    // 检测单个值, 用于界面提示
    validateAllValue(value)

    // 循环遍历每列数据, 进行校检
    const validator = new Schema(rules)
    const validators = value.map((item) => {
      return new Promise((resolve, reject) => {
        return validator.validate(item).then(resolve).catch(reject)
      })
    })
    return Promise.all(validators)
  }
  // 检测每一个行的每个值的数据
  const validateAllValue = (value: any[] | undefined) => {
    const ruleKeys = Object.keys(rules || {})
    value && value.forEach((item: { [x: string]: any }, index: any) => {
      ruleKeys.forEach(async (prop) => {
        await validateOneValue(prop, index, item[prop])
      })
    })
  }
  // 检测单个数据
  const validateOneValue = (prop: string, index: any, value: any) => {
    return new Promise(async (resolve) => {
      try {
        // 参数校检
        await checkValue(prop, value)
        handleCheckSuccess(prop, index)
        resolve(true)
      } catch (errors) {
        // 处理错误
        handleCheckError(prop, index, errors)
        resolve(false)
      }
    })
  }
  // 数据校检
  const checkValue = (prop: string | number, value: any) => {
    return new Promise((resolve, reject) => {
      // 如果校检规则存在, 且当前字段的校检规则存在
      if (rules && rules[prop]) {
        const validator = new Schema({ [prop]: rules[prop] })
        validator.validate({ [prop]: value }, (errors: any, fields: any) => {
          if (errors) {
            reject({ errors, fields })
          } else {
            resolve(true)
          }
        })
      } else {
        resolve(false)
      }
    })
  }
  // 检查通过
  // 重置 state.errorList 的值
  const handleCheckSuccess = (prop: string | number, index: string | number) => {
    if (state.errorList[index] && state.errorList[index][prop]) {
      state.errorList[index][prop] = null
    }
  }
  // 处理错误, 将错误拼接字符串, 并保存在 errorList 中
  const handleCheckError = (prop, index, errors) => {
    if (!state.errorList[index]) {
      state.errorList[index] = {}
    }
    state.errorList[index][prop] = errors.map((item) => item.message).join(',')
  }
  // 判断是否出错
  // 用于加 class 样式和 tooltip 的 disabled 属性
  const isError = (index, valueKey, prop) => {
    return state.errorList &&
      state.errorList[index] &&
      state.errorList[index][valueKey || prop]
  }
  return [
    validate,
    {isError,validateOneValue}
  ]
}
