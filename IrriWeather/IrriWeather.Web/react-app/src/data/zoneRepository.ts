﻿import ZoneApiModel from './api-models/ZoneApiModel';
import AddZoneApiModel from 'src/data/api-models/AddZoneApiModel';


export class ZoneRepository {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/zones";
    }

    public async getAll(): Promise<ZoneApiModel[]> {
        try {
            //let zones = new Array<ZoneApiModel>();
            ////zones.push({
            ////    id: "1",
            ////    channel: 2,
            ////    name: 'my first zone',
            ////    description: 'first zone to be created',
            ////    isEnabled: true,
            ////    isStarted: true
            ////});
            ////return zones;
            let response = await fetch(this.baseUrl);
            if (!response.ok)
                throw new DOMException(`Error fetching zones: ${response.statusText}`);
            let payload = await response.json();

            var zones = payload as ZoneApiModel[];
            return zones;
        } catch (error) {
            throw new Error('Failed to retrieve zone list'); 
        }
    }

    public async getById(id: string): Promise<ZoneApiModel | null> {
        try {
            let response = await fetch(this.baseUrl + "/" + id);
            if (!response.ok)
                throw new DOMException(`Error fetching zones: ${response.statusText}`);
            let payload = await response.json();

            var zone = payload as ZoneApiModel;
            return zone;
        } catch (error) {
            throw new Error('Failed to retrieve zone list');
        }
    }


    public async add(zone: AddZoneApiModel): Promise<ZoneApiModel | null> {

        let response = await fetch(this.baseUrl, {
            method: "post",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(zone),
        }, );

        if (!response.ok)
            throw new DOMException(`Error fetching zones: ${response.statusText}`);

        let payload = await response.json();

        return payload as ZoneApiModel;
    }

}

