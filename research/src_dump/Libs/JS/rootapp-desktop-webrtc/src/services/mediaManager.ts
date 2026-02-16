import {IMediaManager} from '../types';

export class MediaManager implements IMediaManager {
  /**
   * Gets the list of devices of the specified kind(s) as a JSON string.
   * If no kind is specified, all devices are returned
   * @param kind
   */
  public async getDevices(kind?: MediaDeviceKind | MediaDeviceKind[]): Promise<string> {
    const kinds: MediaDeviceKind[] = kind ? typeof kind === 'string' ? [kind] : kind : ['audioinput', 'videoinput', 'audiooutput'];
    try {
      await this.promptUserForDevicesIfNeeded(kinds);
      const devices = await navigator.mediaDevices.enumerateDevices();
      return JSON.stringify(devices.filter(device => kinds.includes(device.kind)));
    } catch (e) {
      console.error(`Failed to get devices of kind(s) ${kinds.join(', ')}`, e);
      return '';
    }
  }

  /**
   * Prompts the user for permission to use the requested Input Devices if we don't already have it
   * @param kinds
   * @private
   */
  private async promptUserForDevicesIfNeeded(kinds: MediaDeviceKind[]) {
    for (const kind of kinds.filter(mediaKind => mediaKind !== 'audiooutput')) {
      const permission = await navigator.permissions.query({name: (
        kind === 'audioinput' ? 'microphone' as const : 'camera' as const
        ) as PermissionName});
      if (permission.state == 'prompt' as const) {
        const stream = await navigator.mediaDevices.getUserMedia({
            'audio': true,
            'video': true
          }
        );
        stream.getTracks().forEach((track) => track.stop());
      }
    }
  }
}

window.__mediaManager = new MediaManager();
