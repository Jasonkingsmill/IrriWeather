import ZoneApiModel from './api-models/ZoneApiModel';
import AddZoneApiModel from 'src/data/api-models/AddZoneApiModel';


export class ZoneService {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/zones";
    }


    public async startZone(id: string): Promise<void> {
        try {
            let response = await fetch(this.baseUrl + "/" + id + "/start", {
                method: "post",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok)
                throw new DOMException(`Error starting zone: ${response.statusText}`);
        } catch (error) {
            throw new Error('Failed to retrieve zone list');
        }
    }

    public async stopZone(id: string): Promise<void> {
        try {
            let response = await fetch(this.baseUrl + "/" + id + "/stop", {
                method: "post",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok)
                throw new DOMException(`Error starting zone: ${response.statusText}`);
        } catch (error) {
            throw new Error('Failed to retrieve zone list');
        }
    }


}

