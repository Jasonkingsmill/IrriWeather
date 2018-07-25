import ZoneApiModel from './api-models/ZoneApiModel';


export class ZoneRepository {
    baseUrl: string = '';

    constructor(url: string) {
        this.baseUrl = url;
    }

    public async getAll(): Promise<ZoneApiModel[]> {
        try {
            let response = await fetch(this.baseUrl + 'zone');
            if (!response.ok)
                throw new DOMException("Response failed");
            let data = await response.json() as ZoneApiModel[];
            return data;
        } catch (error) {
            throw new Error('Failed to retrieve zone list'); 
        }
    }

    public async getById(id: number): Promise<ZoneApiModel | null> {
        if (id == undefined) { 
            console.log('Client ID must be valid');
            return null;
        }

        let dto = await this.fetchClientDetails(id);
        if (dto == undefined)
            throw new Error('Client not found');

        let client = DataMapper.mapClientApiModelToClient(dto);
         
        return client;
    }

    public async add(zone: ZoneApiModel) {
        console.log('Adding zone to repository');
        try {
            let response = await fetch(this.baseUrl + 'zones', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(zone)
            });
            let data = await response.json() as ApiResponseModel;
            if (data.code >= 200 && data.code < 300) {
                let client = Object.assign({}, data.payload);
                return client;
            } else {
                return {};
            }
        } catch (error) {
            console.log('Create client failed. ' + error);
        }
    
    }
}

