import {  FeaturesServiceProxy, GetFeatureListResultInput,UpdateFeatureInput,UpdateFeaturesDto} from '/@/services/ServiceProxies';

export async function getHostFeatureListAsync() {
    const _featuresServiceProxy = new FeaturesServiceProxy();
    const request = new GetFeatureListResultInput();
    request.providerName = 'T';
    return await _featuresServiceProxy.list(request);
  }
  
  export async function updateHostFeatureListAsync(params) {
    const _featuresServiceProxy = new FeaturesServiceProxy();
    const request = new UpdateFeatureInput();

    request.providerName = 'T';
    request.updateFeaturesDto= new UpdateFeaturesDto();
    request.updateFeaturesDto.features=params;
    return await _featuresServiceProxy.update(request);
  }
  