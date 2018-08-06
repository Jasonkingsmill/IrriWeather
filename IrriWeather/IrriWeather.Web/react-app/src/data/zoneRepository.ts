import ZoneApiModel from './api-models/ZoneApiModel';


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
            return payload;
        } catch (error) {
            throw new Error('Failed to retrieve zone list'); 
        }
    }

    public async getById(id: number): Promise<ZoneApiModel | null> {
        if (id == undefined) { 
            console.log('Client ID must be valid');
            return null;
        }

        //let dto = await this.fetchClientDetails(id);
        //if (dto == undefined)
        //    throw new Error('Client not found');

        //let client = DataMapper.mapClientApiModelToClient(dto);

        let client = {} as ZoneApiModel;
        return client;
    }

}

