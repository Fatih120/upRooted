export const EFFECTS_SDK_CUSTOMER_ID = 'bdbf5ca934052375c5ff881f559742e3debe5701';
export const EFFECTS_SDK_CONFIG ={
  preset: 'balanced',
  sample_rate: 48000,
  processingChunk: 64,
  sdk_url: 'rootapp://webrtc/suppresion/',
  processorType: 'worklet',
  wasmPaths: {
    'ort-wasm.wasm': 'rootapp://webrtc/suppresion/ort-wasm.wasm',
    'ort-wasm-simd.wasm': 'rootapp://webrtc/suppresion/ort-wasm-simd.wasm'
  }
};