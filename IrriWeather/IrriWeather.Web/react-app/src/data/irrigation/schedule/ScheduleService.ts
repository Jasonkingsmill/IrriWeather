

export class ScheduleService {
    baseUrl: string = '';

    constructor() {
        this.baseUrl = window.location.origin + "/api/irrigation/schedules";
    }


    public async startSchedule(id: string): Promise<void> {
        try {
            let response = await fetch(this.baseUrl + "/" + id + "/start", {
                method: "post",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok)
                throw new DOMException(`Error starting schedule: ${response.statusText}`);
        } catch (error) {
            throw new Error('Failed to retrieve schedule list');
        }
    }

    public async stopSchedule(id: string): Promise<void> {
        try {
            let response = await fetch(this.baseUrl + "/" + id + "/stop", {
                method: "post",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok)
                throw new DOMException(`Error starting schedule: ${response.statusText}`);
        } catch (error) {
            throw new Error('Failed to retrieve schedule list');
        }
    }


}

