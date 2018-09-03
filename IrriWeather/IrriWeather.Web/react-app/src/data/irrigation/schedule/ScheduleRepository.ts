import ScheduleApiModel from "src/data/irrigation/schedule/api-models/ScheduleApiModel";
import AddScheduleApiModel from "src/data/irrigation/schedule/api-models/AddScheduleApiModel";
import { ScheduleType } from "src/data/irrigation/schedule/api-models/ScheduleType";
import Schedule from "src/data/irrigation/schedule/Schedule";
import { TimeSpan } from "src/data/TimeSpan";



export class ScheduleRepository {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/schedules";
    }

    public async getAll(): Promise<Schedule[]> {
        try {
            let response = await fetch(this.baseUrl);
            if (!response.ok)
                throw new DOMException(`Error fetching schedules: ${response.statusText}`);
            let payload = await response.json();

            var schedules = payload as Schedule[];
            return schedules;
        } catch (error) {
            throw new Error('Failed to retrieve schedule list'); 
        }
    }

    public async getById(id: string): Promise<Schedule | null> {
        try {
            let response = await fetch(this.baseUrl + "/" + id);
            if (!response.ok)
                throw new DOMException(`Error fetching schedule: ${response.statusText}`);
            let payload = await response.json() as ScheduleApiModel;
            var schedule = {
                id: payload.id,
                name: payload.name,
                enabledUntil: payload.enabledUntil,
                isEnabled: payload.isEnabled,
                scheduleType: payload.scheduleType,
                startDate: payload.startDate,
                startTime: payload.startTime,
                zoneIds: payload.zoneIds,
                days: payload.days,
                description: payload.description,
                duration: payload.duration,
            } as Schedule;
            return schedule;
        } catch (error) {
            throw new Error('Failed to fetch schedule');
        }
    }


    public async add(schedule: Schedule): Promise<Schedule | null> {
        let endpoint = this.getEndpoint(schedule.scheduleType);
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

        let payload = await response.json() as ScheduleApiModel;
        var schedule = {
            id: payload.id,
            name: payload.name,
            enabledUntil: payload.enabledUntil,
            isEnabled: payload.isEnabled,
            scheduleType: payload.scheduleType,
            startDate: payload.startDate,
            startTime: payload.startTime,
            zoneIds: payload.zoneIds,
            days: payload.days,
            description: payload.description,
            duration: payload.duration,
        } as Schedule;
        return schedule;
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


    public async update(id: string, schedule: Schedule): Promise<Schedule | null> {

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

        return payload as Schedule;
    }



    private getEndpoint(scheduleType: ScheduleType): string {
        switch (scheduleType) {
            case ScheduleType.DateTime:
                return "datetime";
            case ScheduleType.DaysOfMonth:
                return "daysofmonth";
            case ScheduleType.DaysOfWeek:
                return "daysofweek";
            case ScheduleType.EvenDays:
                return "evendays";
            case ScheduleType.OddDays:
                return "odddays";
        }
    }
}

