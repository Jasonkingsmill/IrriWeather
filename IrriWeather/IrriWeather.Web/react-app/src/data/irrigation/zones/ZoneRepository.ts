import ZoneApiModel from './api-models/ZoneApiModel';
import AddZoneApiModel from 'src/data/irrigation/zones/api-models/AddZoneApiModel';


export class ZoneRepository {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/zones";
    }

    public async getAll(): Promise<ZoneApiModel[]> {
        try {
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
                throw new DOMException(`Error fetching zone: ${response.statusText}`);
            let payload = await response.json();

            var zone = payload as ZoneApiModel;
            return zone;
        } catch (error) {
            throw new Error('Failed to fetch zone');
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
            throw new DOMException(`Error adding zone: ${response.statusText}`);

        let payload = await response.json();

        return payload as ZoneApiModel;
    }


    public async remove(id: string): Promise<void | null> {

        let response = await fetch(this.baseUrl + "/" + id, {
            method: "delete",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        }, );

        if (!response.ok)
            throw new DOMException(`Error removing zone: ${response.statusText}`);

    }


    public async update(id: string, zone: AddZoneApiModel): Promise<ZoneApiModel | null> {

        let response = await fetch(this.baseUrl + "/" + id, {
            method: "put",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(zone),
        }, );

        if (!response.ok)
            throw new DOMException(`Error update zone: ${response.statusText}`);

        let payload = await response.json();

        return payload as ZoneApiModel;
    }

}

