import ScheduleApiModel from "src/data/irrigation/schedule/api-models/ZoneApiModel";
import AddScheduleApiModel from "src/data/irrigation/schedule/api-models/AddScheduleApiModel";



export class ScheduleRepository {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/schedules";
    }

    public async getAll(): Promise<ScheduleApiModel[]> {
        try {
            let response = await fetch(this.baseUrl);
            if (!response.ok)
                throw new DOMException(`Error fetching schedules: ${response.statusText}`);
            let payload = await response.json();

            var schedules = payload as ScheduleApiModel[];
            return schedules;
        } catch (error) {
            throw new Error('Failed to retrieve schedule list'); 
        }
    }

    public async getById(id: string): Promise<ScheduleApiModel | null> {
        try {
            let response = await fetch(this.baseUrl + "/" + id);
            if (!response.ok)
                throw new DOMException(`Error fetching schedule: ${response.statusText}`);
            let payload = await response.json();

            var schedule = payload as ScheduleApiModel;
            return schedule;
        } catch (error) {
            throw new Error('Failed to fetch schedule');
        }
    }


    public async add(schedule: AddScheduleApiModel): Promise<ScheduleApiModel | null> {

        let response = await fetch(this.baseUrl, {
            method: "post",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(schedule),
        }, );

        if (!response.ok)
            throw new DOMException(`Error adding schedule: ${response.statusText}`);

        let payload = await response.json();

        return payload as ScheduleApiModel;
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
            throw new DOMException(`Error removing schedule: ${response.statusText}`);

    }


    public async update(id: string, schedule: AddScheduleApiModel): Promise<ScheduleApiModel | null> {

        let response = await fetch(this.baseUrl + "/" + id, {
            method: "put",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(schedule),
        }, );

        if (!response.ok)
            throw new DOMException(`Error update schedule: ${response.statusText}`);

        let payload = await response.json();

        return payload as ScheduleApiModel;
    }

}

